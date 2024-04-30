using Acme.Greenhouse.Nodes;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.Greenhouse.EntityFrameworkCore
{
    public interface IGreenhouseDbContext : IEfCoreDbContext
    {
        public DbSet<Node> Nodes { get; }
        public DbSet<Sensor> Sensors { get; }
        public DbSet<Device> Devices { get; }
        public DbSet<NodeStatus> NodeStatus { get; }
        public DbSet<SensorData> SensorData { get; }
        public DbSet<DeviceStatus> DeviceStatus { get; }
    }
}
