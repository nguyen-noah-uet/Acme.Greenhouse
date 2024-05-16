using Acme.Greenhouse.Sensors;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Greenhouse.SensorData
{
    public class SensorData : CreationAuditedEntity<int>
    {
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; } = default!;
        public double Value { get; set; }
    }
}
