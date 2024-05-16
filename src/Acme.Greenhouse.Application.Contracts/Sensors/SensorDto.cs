using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.Sensors
{
    [Serializable]
    public class SensorDto : AuditedEntityDto<int>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public int NodeId { get; set; }
        public double HighThreshold { get; set; }
        public double LowThreshold { get; set; }
        public SensorType SensorType { get; set; }
        public string? Unit { get; set; }
        public override string ToString()
        {
            return $"Sensor [Id={Id},Name={Name}]";
        }
    }
}
