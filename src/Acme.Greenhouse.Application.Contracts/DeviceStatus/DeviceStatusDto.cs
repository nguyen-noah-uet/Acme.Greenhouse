using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.DeviceStatus
{
    [Serializable]
    public class DeviceStatusDto : CreationAuditedEntityDto<int>
    {
        public int DeviceId { get; set; } = default!;
        public bool IsOn { get; set; }
    }
}
