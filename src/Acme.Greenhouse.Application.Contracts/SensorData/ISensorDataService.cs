using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.SensorData
{
    public interface ISensorDataService : ICrudAppService<SensorDataDto, int, PagedAndSortedResultRequestDto, SensorDataCreateDto>
    {
        Task<List<SensorDataDto>> GetByDate(int sensorId, DateTime from, DateTime to);
    }
}
