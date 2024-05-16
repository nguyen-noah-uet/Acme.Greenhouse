using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;
using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Sensors;

namespace Acme.Greenhouse.Nodes
{
    public class Node : AuditedAggregateRoot<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; } = default!;

        public ICollection<Sensor> Sensors { get; set; } = default!;
        public ICollection<Device> Devices { get; set; } = default!;
    }
}
