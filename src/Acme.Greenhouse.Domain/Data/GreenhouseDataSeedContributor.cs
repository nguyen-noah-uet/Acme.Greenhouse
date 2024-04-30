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
                var controlNode = await nodeRepo.InsertAsync(new Node() { Name = "Control", Description = "Node for control pump relays" }, autoSave:true);
                var sensorNode1 = await nodeRepo.InsertAsync(new Node() { Name = "Sensors 1", Description = "Node for soil moisture sensor" }, autoSave: true);
                var sensorNode2 = await nodeRepo.InsertAsync(new Node() { Name = "Sensors 2", Description = "Node for pH, EC sensors" }, autoSave: true);

                // seed 3 relay devices
                var relay1 = await deviceRepo.InsertAsync(new Device() { Name = "Relay 1", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);
                var relay2 = await deviceRepo.InsertAsync(new Device() { Name = "Relay 2", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);
                var relay3 = await deviceRepo.InsertAsync(new Device() { Name = "Relay 3", DeviceType = DeviceType.Relay, NodeId = controlNode.Id }, autoSave: true);

                // seed soil moisture sensor
                var soilMoistureSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "Soil Moisture", SensorType = SensorType.SoilMoisture, Unit = "%", LowThreshold = 75.0, HighThreshold = 80.0, NodeId = sensorNode1.Id }, autoSave: true);

                // seed pH, EC sensors
                var phSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "pH", SensorType = SensorType.Ph, LowThreshold = 6.0, HighThreshold = 6.5, NodeId = sensorNode2.Id }, autoSave: true);
                var ecSensor = await sensorRepo.InsertAsync(new Sensor() { Name = "EC", SensorType = SensorType.Ec, Unit = "µS/cm", LowThreshold = 200.0, HighThreshold = 700.0, NodeId = sensorNode2.Id }, autoSave: true);
                logger.LogInformation("Data inserted.");
            }
        }
    }
}
