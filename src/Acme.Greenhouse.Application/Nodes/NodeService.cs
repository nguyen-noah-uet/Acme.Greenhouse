using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.Nodes
{
    public class NodeService
        : CrudAppService<Node, NodeDto, int, PagedAndSortedResultRequestDto, NodeCreateUpdateDto, NodeCreateUpdateDto>,
        INodeService
    {
        private readonly IRepository<Node, int> repository;

        public NodeService(IRepository<Node, int> repository) : base(repository)
        {
            this.repository = repository;
        }
        //public async Task<List<NodeDto>> GetListDetailedAsync()
        //{
        //    var query = await repository.GetQueryableAsync();
        //    query.
        //}
    }

    public class DeviceService
        : CrudAppService<Device, DeviceDto, int, PagedAndSortedResultRequestDto, DeviceCreateUpdateDto, DeviceCreateUpdateDto>,
        IDeviceService
    {
        private readonly IRepository<Device, int> repository;

        public DeviceService(IRepository<Device, int> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<List<DeviceDto>> GetByNodeId(int nodeId)
        {
            var queryable = await repository.GetQueryableAsync();
            var entities = queryable.Where(s => s.NodeId == nodeId).ToList();
            return await MapToGetListOutputDtosAsync(entities);
        }
    }

    public class SensorService
        : CrudAppService<Sensor, SensorDto, int, PagedAndSortedResultRequestDto, SensorCreateUpdateDto, SensorCreateUpdateDto>,
        ISensorService
    {
        private readonly IRepository<Sensor, int> repository;

        public SensorService(IRepository<Sensor, int> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<List<SensorDto>> GetByNodeId(int nodeId)
        {
            var queryable = await repository.GetQueryableAsync();
            var entities = queryable.Where(s => s.NodeId == nodeId).ToList();
            return await MapToGetListOutputDtosAsync(entities);
        }

    }

    public class SensorDataService
        : CrudAppService<SensorData, SensorDataDto, int, PagedAndSortedResultRequestDto, SensorDataCreateDto>,
        ISensorDataService
    {
        private readonly IRepository<SensorData, int> repository;

        public SensorDataService(IRepository<SensorData, int> repository) : base(repository)
        {
            this.repository = repository;
        }

        public async Task<List<SensorDataDto>> GetByDate(int sensorId, DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException("From date cannot be greater than to date");
            }
            var queryable = await repository.GetQueryableAsync();
            var entities = queryable
                .Where(x => x.SensorId == sensorId && x.CreationTime >= from && x.CreationTime <= to)
                .OrderBy(x => x.CreationTime)
                .ToList();
            return await MapToGetListOutputDtosAsync(entities);
        }
    }

    public class NodeStatusService
        : CrudAppService<NodeStatus, NodeStatusDto, int, PagedAndSortedResultRequestDto, NodeStatusCreateDto>,
        INodeStatusService
    {
        public NodeStatusService(IRepository<NodeStatus, int> repository) : base(repository)
        {
        }
    }

    public class DeviceStatusService
        : CrudAppService<DeviceStatus, DeviceStatusDto, int, PagedAndSortedResultRequestDto, DeviceStatusCreateDto>,
        IDeviceStatusService
    {
        public DeviceStatusService(IRepository<DeviceStatus, int> repository) : base(repository)
        {
        }
    }

    public class DbLogService : CrudAppService<DbLog, DbLogDto, int, PagedAndSortedResultRequestDto, DbLogCreateDto>, 
        IDbLogService
    {
        public DbLogService(IRepository<DbLog, int> repository) : base(repository)
        {
        }
    }
}
