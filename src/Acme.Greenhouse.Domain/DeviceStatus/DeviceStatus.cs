using Acme.Greenhouse.Devices;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.Greenhouse.DeviceStatus
{
    public class DeviceStatus : CreationAuditedEntity<int>
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; } = default!;
        public bool IsOn { get; set; }
    }
}
