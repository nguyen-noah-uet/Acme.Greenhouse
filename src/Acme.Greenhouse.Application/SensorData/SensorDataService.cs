using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace Acme.Greenhouse.SensorData
{
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
}
