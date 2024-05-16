using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.Sensors
{
    public interface ISensorService :
        ICrudAppService<SensorDto, int, PagedAndSortedResultRequestDto, SensorCreateUpdateDto, SensorCreateUpdateDto>
    {
        Task<List<SensorDto>> GetByNodeId(int nodeId);
    }
}
