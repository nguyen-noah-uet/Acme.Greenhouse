using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.Greenhouse.Sensors
{
    [Serializable]
    public class SensorCreateUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; } = default!;
        public int NodeId { get; set; }
        public double HighThreshold { get; set; }
        public double LowThreshold { get; set; }
        public SensorType SensorType { get; set; }
        [MaxLength(20)]
        public string? Unit { get; set; }
    }
}
