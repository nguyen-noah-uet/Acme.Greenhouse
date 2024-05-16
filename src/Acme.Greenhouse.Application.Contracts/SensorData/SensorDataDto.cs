using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.SensorData
{
    [Serializable]
    public class SensorDataDto : CreationAuditedEntityDto<int>
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
    }
}
