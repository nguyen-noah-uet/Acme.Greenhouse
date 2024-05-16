using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.Devices
{
    public interface IDeviceService :
        ICrudAppService<DeviceDto, int, PagedAndSortedResultRequestDto, DeviceCreateUpdateDto, DeviceCreateUpdateDto>
    {
        Task<List<DeviceDto>> GetByNodeId(int nodeId);
    }
}
