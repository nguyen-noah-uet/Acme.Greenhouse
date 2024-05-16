using System;

namespace Acme.Greenhouse.DeviceStatus
{
    [Serializable]
    public class DeviceStatusCreateDto
    {
        public int DeviceId { get; set; } = default!;
        public bool IsOn { get; set; }
    }
}
