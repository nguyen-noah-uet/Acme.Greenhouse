using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.Greenhouse.Blazor.Middlewares;
using Acme.Greenhouse.Data;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using Serilog;
using Serilog.Events;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace Acme.Greenhouse.Blazor;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
            .UseAutofac()
                .UseSerilog();
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped<ITooltipService, TooltipService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IPAddressMiddleware>();
            builder.Services.AddHttpClient();
            builder.Services.AddFluentUIComponents();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddHostedService<StoreDataBackgroundService>();
            builder.Services.AddSingleton<IManagedMqttClient>((_) =>
            {
                Log.Information("Connecting to MQTT broker. {0} {1}", builder.Configuration["Mqtt:ClientId"], builder.Configuration["Mqtt:Server"]);

                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithClientId(builder.Configuration["Mqtt:ClientId"])
                    .WithTcpServer(builder.Configuration["Mqtt:Server"])
                    .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V310)
                    .WithKeepAlivePeriod(TimeSpan.FromSeconds(60))
                    //.WithCredentials(builder.Configuration["Mqtt:User"], builder.Configuration["Mqtt:Password"])
                    .WithCleanSession()
                    .WithTimeout(TimeSpan.FromSeconds(5))
                    .Build();
                var managedMqttClientOptions = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                    .WithClientOptions(mqttClientOptions)
                    .Build();
                var client = new MqttFactory().CreateManagedMqttClient();
                client.ConnectedAsync += e =>
                {
                    Log.Information("Connected to MQTT broker. {0} {1}", builder.Configuration["Mqtt:ClientId"], builder.Configuration["Mqtt:Server"]);
                    return Task.CompletedTask;
                };
                client.DisconnectedAsync += e =>
                {
                    Log.Error("Disconnected to MQTT broker.  {0} {1}", builder.Configuration["Mqtt:ClientId"], builder.Configuration["Mqtt:Server"]);
                    return Task.CompletedTask;
                };
                client.ConnectingFailedAsync += e =>
                {
                    Log.Error("Failed to reconnect to MQTT broker. {0} {1}", builder.Configuration["Mqtt:ClientId"], builder.Configuration["Mqtt:Server"]);
                    return Task.CompletedTask;
                };
                client.StartAsync(managedMqttClientOptions).Wait();
                client.SubscribeAsync("sensors/#").Wait();
                client.SubscribeAsync("command/#").Wait();
                client.SubscribeAsync("node-status/#").Wait();
                client.SubscribeAsync("device-status/#").Wait();
                client.SubscribeAsync("mode-node/#").Wait();


                return client;
                    
            });
            await builder.AddApplicationAsync<GreenhouseBlazorModule>();
            var app = builder.Build();
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxccHZXQ2JYVk1+X0I=");
            app.Services.GetRequiredService<IManagedMqttClient>();
            // Run db migration
            try
            {
                var dbMigrationSerivce = app.Services.GetRequiredService<GreenhouseDbMigrationService>();
                await dbMigrationSerivce.MigrateAsync();
            }
            finally
            {
                await app.InitializeApplicationAsync();
                await app.RunAsync();
            }

            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException)
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
