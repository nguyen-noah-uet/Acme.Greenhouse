using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.Greenhouse.Nodes;

namespace Acme.Greenhouse.Devices
{
    public class Device : AuditedAggregateRoot<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; } = default!;
        public int NodeId { get; set; }
        public Node Node { get; set; } = default!;
        public DeviceType DeviceType { get; set; }
    }
}
