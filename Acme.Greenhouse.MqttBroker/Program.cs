

using Microsoft.AspNetCore.Http.HttpResults;
using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.Diagnostics;
using MQTTnet.Server;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(1883, configure => configure.UseMqtt());
    options.ListenAnyIP(1884);
});

builder.Services.AddHostedMqttServer(options =>
{
    options.WithDefaultEndpointPort(1883);
    options.WithoutDefaultEndpoint();
})
    .AddMqttConnectionHandler()
    .AddConnections()
    .AddMqttLogger(new MqttNetEventLogger());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMqttServer(server =>
{
    server.StartedAsync += args =>
    {
        _ = Task.Run(async () =>
        {
            var mqttApplicationMessage = new MqttApplicationMessageBuilder()
                .WithPayload($"Test application message from MQTTnet server.")
                .WithTopic("message")
                .Build();
            
            while (true)
            {
                try
                {
                    await server.InjectApplicationMessage(new InjectedMqttApplicationMessage(mqttApplicationMessage)
                    {
                        SenderClientId = "server"
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }
        });

        return Task.CompletedTask;
    };
    server.ValidatingConnectionAsync += Task (e) =>
    {
        return Task.CompletedTask;
    };
    server.ClientConnectedAsync += Task (e) => Task.CompletedTask;
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.MapMqtt("/mqtt");
app.MapGet("/mqtt-clients", async (MqttServer server) =>
{
    var clients = await server.GetClientsAsync();
    return Results.Ok(clients);
});
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
