using Acme.Greenhouse.Blazor.ViewModels;
using Acme.Greenhouse.Settings;
using Microsoft.Extensions.Logging;
using MQTTnet.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using MQTTnet;
using System.Text;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;
using Microsoft.FluentUI.AspNetCore.Components.DesignTokens;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Acme.Greenhouse.Blazor.Pages;

public partial class Dashboard
{
    private List<NodeViewModel> _nodes = new();
    private List<string> _logs = new();
    public bool Loaded { get; set; }

    private bool autoMode=true;
    public bool AutoMode
    {
        get { return autoMode; }
        set 
        { 
            autoMode = value;
        }
    }
    string? _searchValue = string.Empty;
    FluentToolbar? Toolbar;
    protected override async Task OnInitializedAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/Account/Login", true);
            return;
        }

        try
        {
            await LoadNodes();
            await LoadDbLogs();
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }

        try
        {
            MqttService.ApplicationMessageReceivedAsync -= OnMessageReceived;
            MqttService.ApplicationMessageReceivedAsync += OnMessageReceived;
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
        Loaded = true;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        //if (firstRender)
        //{
        //    await FillColor.SetValueFor(Toolbar!.Element, "#333".ToSwatch());
        //    await BaseLayerLuminance.SetValueFor(Toolbar!.Element, (float)0.15);

        //    StateHasChanged();
        //}
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        MqttService.ApplicationMessageReceivedAsync -= OnMessageReceived;
    }

    private async Task LoadNodes()
    {
        // Load nodes
        var nodes = await NodeService.GetListAsync(new PagedAndSortedResultRequestDto() { Sorting = "id" });
        for (int i = 0; i < nodes.TotalCount; i++)
        {
            var m = new NodeViewModel()
            {
                Node = nodes.Items[i],
                IsOnline = false,
                Sensors = (await SensorService.GetByNodeId(nodes.Items[i].Id)).Select(x => new SensorViewModel() { Sensor = x }).ToList(),
                Devices = (await DeviceService.GetByNodeId(nodes.Items[i].Id)).Select(x => new DeviceViewModel() { Device = x }).ToList()
            };
            _nodes.Add(m);
        }
    }
    private async Task LoadDbLogs()
    {
        // Load logs
        _logs.Clear();
        var logs = await DbLogService.GetListAsync(new() { MaxResultCount = 40, Sorting = "Id DESC" });
        for (var i = logs.Items.Count - 1; i >= 0; i--)
        {
            var log = logs.Items[i];
            _logs.Add(log.ToString());
        }

        await InvokeAsync(StateHasChanged);
    }
    private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        Regex regex;
        Match match;
        var payloadString = e.ApplicationMessage.ConvertPayloadToString();
        var topic = e.ApplicationMessage.Topic!;
        // mode-node
        regex = new(@"mode-node\/([0-9]+)\/");
        match = regex.Match(topic);
        if (match.Success)
        {
            var mode = payloadString;
            int nodeId = int.Parse(match.Groups[1].Value);
            var node = _nodes.FirstOrDefault(x => x.Node.Id == nodeId);
            if (node != null)
            {
                bool currentMode = mode == "1";
                if (AutoMode != currentMode)
                {
                    AutoMode = currentMode;
                    var message = AutoMode ? "Changed MANUAL_MODE to AUTO_MODE" : "Changed AUTO_MODE to MANUAL_MODE";
                    await DbLogService.CreateAsync(new() { Message = message });
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
    }


    private async void TurnOnDevice(DeviceViewModel device)
    {
        try
        {
            await MqttService.EnqueueAsync(new MqttApplicationMessage() { Topic = $"command/{device.Device.Id}/", PayloadSegment = Encoding.ASCII.GetBytes("1")});
            await DbLogService.CreateAsync(new() { Message = $"Sent turn on command to {device.Device.ToString()}" });
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
    private async void TurnOffDevice(DeviceViewModel device)
    {
        try
        {
            await MqttService.EnqueueAsync(new MqttApplicationMessage() { Topic = $"command/{device.Device.Id}/", PayloadSegment = Encoding.ASCII.GetBytes("0") });
            await DbLogService.CreateAsync(new() { Message = $"Sent turn off command to {device.Device.ToString()}" });
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.Message);
        }
    }
    private async Task ToggleNodeMode()
    {
        if (AutoMode)
        {
            await MqttService.EnqueueAsync(new MqttApplicationMessage() { Topic = $"change-mode/1/", PayloadSegment = Encoding.ASCII.GetBytes("0") });
            await DbLogService.CreateAsync(new() { Message = $"Sent change AUTO_MODE to MANUAL_MODE command." });
        }
        else
        {
            await MqttService.EnqueueAsync(new MqttApplicationMessage() { Topic = $"change-mode/1/", PayloadSegment = Encoding.ASCII.GetBytes("1") });
            await DbLogService.CreateAsync(new() { Message = $"Sent change MANUAL_MODE to AUTO_MODE command." });
        }
    }

    private void HandleSearchInput()
    {
        ShowToast();
    }

    void ShowToast()
    {
        Random rnd = new();

        var intent = Enum.GetValues<ToastIntent>()[rnd.Next(10)];
        var message = $"Simple Toast";
        ToastService.ShowToast(intent, message);
    }

    private void ClearLog()
    {
        _logs.Clear();
    }

    private async Task RefreshLog()
    {
        await LoadDbLogs();
    }
    private async Task OpenDialogAsync()
    {
        await DialogService.ShowDialogAsync<FlowchartDialog>(new DialogParameters()
        {
            OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async (instance) =>
            {
                await JsRuntime.InvokeVoidAsync("eval", $@"
                        async function func() {{
                            let dialog = document.getElementById('{instance.Id}')?.dialog;
                            if (!dialog) return;
                            dialog.style.opacity = '0.0';
                            let animation = dialog.animate([
                                {{ opacity: '1', transform: '' }},
                                {{ opacity: '0', transform: 'translateX(-100%)' }}
                            ], {{
                                duration: 200,
                            }});
                            return animation.finished; // promise that resolves when the animation is complete or interrupted
                        }};
                        func();
                    ");
            }),
            OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async (instance) =>
            {
                await JsRuntime.InvokeVoidAsync("eval", $@"
                        async function func() {{
                            let dialog = document.getElementById('{instance.Id}')?.dialog;
                            if (!dialog) return;
                            let animation = dialog.animate([
                                {{ opacity: '0', transform: 'translateX(-100%)' }},
                                {{ opacity: '1', transform: '' }},
                            ], {{
                                duration: 200,
                            }});
                            return animation.finished; // promise that resolves when the animation is complete or interrupted
                        }};
                        func();
                    ");
            }),
            Width = "60%",
        });
    }
}
