using System;
using System.Collections.Generic;
using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Sensors;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.Nodes
{
    [Serializable]
    public class NodeDto : AuditedEntityDto<int>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<SensorDto> Sensors { get; set; } = default!;
        public ICollection<DeviceDto> Devices { get; set; } = default!;
        public override string ToString()
        {
            return $"Node [Id={Id},Name={Name}]";
        }
    }
}
