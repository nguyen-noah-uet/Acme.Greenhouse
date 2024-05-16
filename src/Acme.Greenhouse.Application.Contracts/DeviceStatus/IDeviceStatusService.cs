using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.DeviceStatus
{
    public interface IDeviceStatusService : ICrudAppService<DeviceStatusDto, int, PagedAndSortedResultRequestDto, DeviceStatusCreateDto>
    {
    }
}
