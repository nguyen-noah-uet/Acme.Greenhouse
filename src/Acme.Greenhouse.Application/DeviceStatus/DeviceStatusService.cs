using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.DeviceStatus
{
    public class DeviceStatusService
        : CrudAppService<DeviceStatus, DeviceStatusDto, int, PagedAndSortedResultRequestDto, DeviceStatusCreateDto>,
        IDeviceStatusService
    {
        public DeviceStatusService(IRepository<DeviceStatus, int> repository) : base(repository)
        {
        }
    }
}
