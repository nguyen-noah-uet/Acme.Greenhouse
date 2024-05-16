using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.Sensors
{
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
}
