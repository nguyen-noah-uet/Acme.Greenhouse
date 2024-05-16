using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Greenhouse.Devices
{
    [Serializable]
    public class DeviceCreateUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; }
        public DeviceType DeviceType { get; set; }
        public int NodeId { get; set; }
    }
}
