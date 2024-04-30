using Volo.Abp.Domain.Repositories;

namespace Acme.Greenhouse.Nodes
{
    public interface INodeRepository : IBasicRepository<Node, int>
    {
    }

    public interface IDeviceRepository : IRepository<Device, int>
    {
    }
    public interface ISensorRepository : IRepository<Sensor, int>
    {
    }
}
