using Acme.Greenhouse.Nodes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Acme.Greenhouse.Extensions;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.Blazor
{
    public class StoreDataBackgroundService : BackgroundService
    {
        private readonly ILogger<StoreDataBackgroundService> _logger;
        private readonly IManagedMqttClient _mqttService;
        private readonly ISensorDataService _sensorDataService;
        private readonly ISensorService _sensorService;
        private readonly IDeviceStatusService _deviceStatusService;
        private readonly IDeviceService _deviceService;
        private readonly INodeStatusService _nodeStatusService;
        private readonly INodeService _nodeService;
        private readonly IDbLogService _dbLogService;

        public StoreDataBackgroundService(
            ILogger<StoreDataBackgroundService> logger,
            IManagedMqttClient mqttService,
            ISensorDataService sensorDataService,
            ISensorService sensorService,
            IDeviceStatusService deviceStatusService,
            IDeviceService deviceService,
            INodeStatusService nodeStatusService,
            INodeService nodeService,
            IDbLogService dbLogService)
        {
            _logger = logger;
            _mqttService = mqttService;
            _sensorDataService = sensorDataService;
            _sensorService = sensorService;
            this._deviceStatusService = deviceStatusService;
            _deviceService = deviceService;
            this._nodeStatusService = nodeStatusService;
            _nodeService = nodeService;
            _dbLogService = dbLogService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ExecuteAsync StoreDataBackgroundService");
            _mqttService.ApplicationMessageReceivedAsync += (OnMessageReceived);
            return Task.CompletedTask;
        }

        private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                var payloadString = e.ApplicationMessage.ConvertPayloadToString();
                var topic = e.ApplicationMessage.Topic;
                Regex nodeStatusRegex = new(@"node-status\/([a-f,0-9,-]+)\/");
                var nodeStatusMatch = nodeStatusRegex.Match(topic);
                if (nodeStatusMatch.Success)
                {
                    int nodeId = int.Parse(nodeStatusMatch.Groups[1].Value);
                    var nodeStatus = payloadString == "1";
                    var latest = (await _nodeStatusService.GetListAsync(new PagedAndSortedResultRequestDto(){ MaxResultCount = 1, Sorting = "CreationTime DESC" })).Items.FirstOrDefault();
                    if (latest != null && latest.IsOnline != nodeStatus)
                    {
                        await InsertNodeStatus(nodeId, nodeStatus);
                    }
                    return;
                }
                Regex sensorRegex = new(@"sensors\/([a-f,0-9,-]+)\/");
                var sensorMatch = sensorRegex.Match(topic);
                if (sensorMatch.Success)
                {
                    int sensorId = int.Parse(sensorMatch.Groups[1].Value);
                    double sensorValue = double.Parse(payloadString);
                    if (sensorId == 1)
                    {
                        sensorValue = sensorValue.Smooth();
                    }
                    await InsertSensorData(sensorId, sensorValue);
                    return;
                }
                Regex deviceStatusRegex = new(@"device-status\/([a-f,0-9,-]+)\/");
                var deviceStatusMatch = deviceStatusRegex.Match(topic);
                if (deviceStatusMatch.Success)
                {
                    int deviceId = int.Parse(deviceStatusMatch.Groups[1].Value);
                    var deviceStatus = payloadString == "1";
                    // Insert if status changed
                    var latest = (await _deviceStatusService.GetListAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = 1, Sorting = "CreationTime DESC" })).Items.FirstOrDefault();
                    if (latest != null && latest.IsOn != deviceStatus)
                    {
                        await InsertDeviceStatus(deviceId, deviceStatus);
                    }
                    return;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when process MQTT message");
            }

        }

        private async Task InsertNodeStatus(int nodeId, bool nodeStatus)
        {
            try
            {
                NodeStatusCreateDto nodeStatusDto = new()
                {
                    NodeId = nodeId,
                    IsOnline = nodeStatus,
                };
                var result = await _nodeStatusService.CreateAsync(nodeStatusDto);
                var dto = await _nodeService.GetAsync(nodeId);
                await LogDb($"{dto.ToString()}: {(result.IsOnline ? "ON" : "OFF")}");
                _logger.LogInformation("Inserted {@result}", result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when insert node status");
            }
        }

        private async Task LogDb(string? message, LogLevel logLevel = LogLevel.Information, string? stackTrace = null)
        {
            try
            {
                await _dbLogService.CreateAsync(new()
                {
                    LogLevel = logLevel,
                    Message = message,
                    StackTrace = stackTrace
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when insert device status");
            }
        }

        private async Task InsertDeviceStatus(int deviceId, bool deviceStatus)
        {
            try
            {
                DeviceStatusCreateDto deviceStatusDto = new()
                {
                    DeviceId = deviceId,
                    IsOn = deviceStatus,
                };
                var result = await _deviceStatusService.CreateAsync(deviceStatusDto);
                var dto = await _deviceService.GetAsync(deviceId);
                await LogDb($"{dto.ToString()}: {(result.IsOn ? "ON" : "OFF")}");
                _logger.LogInformation("Inserted {@result}", result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when insert device status");
            }
        }

        private async Task InsertSensorData(int sensorId, double sensorValue)
        {
            try
            {
                SensorDataCreateDto sensorDto = new()
                {
                    SensorId = sensorId,
                    Value = sensorValue
                };
                var result = await _sensorDataService.CreateAsync(sensorDto);
                var dto = await _sensorService.GetAsync(sensorId);
                await LogDb($"{dto.ToString()}: {result.Value}");
                _logger.LogInformation("Inserted {@result}", result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when insert sensor data");
            }
        }
    }
}
