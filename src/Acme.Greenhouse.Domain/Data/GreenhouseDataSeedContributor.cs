using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Nodes;
using Acme.Greenhouse.Sensors;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.Greenhouse.Data
{
    public class GreenhouseDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Node, int> nodeRepo;
        private readonly IRepository<NodeStatus, int> nodeStatusRepo;
        private readonly IRepository<Device, int> deviceRepo;
        private readonly IRepository<DeviceStatus, int> deviceStatusRepo;
        private readonly IRepository<Sensor, int> sensorRepo;
        private readonly IRepository<SensorData, int> sensorDataRepo;
        private readonly ILogger<GreenhouseDataSeedContributor> logger;

        public GreenhouseDataSeedContributor(
            IRepository<Node, int> nodeRepo,
            IRepository<NodeStatus, int> nodeStatusRepo,
            IRepository<Device, int> deviceRepo,
            IRepository<DeviceStatus, int> deviceStatusRepo,
            IRepository<Sensor, int> sensorRepo,
            IRepository<SensorData, int> sensorDataRepo,
            ILogger<GreenhouseDataSeedContributor> logger
            )
        {
            this.nodeRepo = nodeRepo;
            this.nodeStatusRepo = nodeStatusRepo;
            this.deviceRepo = deviceRepo;
            this.deviceStatusRepo = deviceStatusRepo;
            this.sensorRepo = sensorRepo;
            this.sensorDataRepo = sensorDataRepo;
            this.logger = logger;
        }
            public async Task SeedAsync(DataSeedContext context)
        {
            if (await nodeRepo.GetCountAsync() == 0)
            {
                logger.LogInformation("Seeding nodes data");
                // seed 3 nodes
                var controlNode = await nodeRepo.InsertAsync(new Node() { Name = "Control Node", Description = "Node for control devices" }, autoSave:true);
                var sensorNode1 = await nodeRepo.InsertAsync(new Node() { Name = "Sensors Node", Description = "Node for sensors" }, autoSave: true);
                //var sensorNode2 = await nodeRepo.InsertAsync(new Node() { Name = "Sensors 2", Description = "Node for pH, EC sensors" }, autoSave: true);

                // seed 3 relay devices
                var light = await deviceRepo.InsertAsync(new Device() { Name = "Light", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);
                var ecPump = await deviceRepo.InsertAsync(new Device() { Name = "EC pump", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);
                var phPump = await deviceRepo.InsertAsync(new Device() { Name = "pH pump", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);
                var oxygenPump = await deviceRepo.InsertAsync(new Device() { Name = "Oxygen pump", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);

                // seed soil moisture sensor
                //var soilMoistureSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "Soil Moisture", SensorType = SensorType.SoilMoisture, Unit = "%", LowThreshold = 75.0, HighThreshold = 80.0, NodeId = sensorNode1.Id }, autoSave: true);

                // seed pH, EC sensors
                var phSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "pH", SensorType = SensorType.Ph, LowThreshold = 6.0, HighThreshold = 7.5, NodeId = sensorNode1.Id }, autoSave: true);
                var ecSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "EC", SensorType = SensorType.Ec, Unit = "µS/cm", LowThreshold = 200.0, HighThreshold = 700.0, NodeId = sensorNode1.Id }, autoSave: true);
                var humiditySensor = await sensorRepo.InsertAsync(new Sensor() { Name = "Humidity", SensorType = SensorType.Humidity, Unit = "%", LowThreshold = 60.0, HighThreshold = 95.0, NodeId = sensorNode1.Id }, autoSave: true);
                var temperatureSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "Temperature", SensorType = SensorType.Temperature, Unit = "°C", LowThreshold = 20.0, HighThreshold = 37.0, NodeId = sensorNode1.Id }, autoSave: true);
                logger.LogInformation("Data inserted.");
            }
        }
    }
}
