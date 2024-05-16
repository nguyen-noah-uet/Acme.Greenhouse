using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.Devices
{
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
}
