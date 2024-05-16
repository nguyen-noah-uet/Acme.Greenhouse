using System;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.Devices
{
    [Serializable]
    public class DeviceDto : AuditedEntityDto<int>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DeviceType DeviceType { get; set; }
        public int NodeId { get; set; }
        public override string ToString()
        {
            return $"Device [Id={Id},Name={Name}]";
        }
    }
}
