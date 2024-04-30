using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.Greenhouse.Sensors;
using Acme.Greenhouse.Devices;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;

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
    public class NodeStatus : CreationAuditedEntity<int>
    {
        public int NodeId { get; set; }
        public Node Node { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
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
    public class DeviceStatus : CreationAuditedEntity<int>
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; } = default!;
        public bool IsOn { get; set; }
    }

    public class Sensor : AuditedAggregateRoot<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; } = default!;
        public int NodeId { get; set; }
        public Node Node { get; set; } = default!;
        public double HighThreshold { get; set; }
        public double LowThreshold { get; set; }
        public SensorType SensorType { get; set; }
        [MaxLength(20)]
        public string? Unit { get; set; }
    }
    public class SensorData : CreationAuditedEntity<int>
    {
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; } = default!;
        public double Value { get; set; }
    }

    public class DbLog : CreationAuditedEntity<int>
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        [MaxLength(1000)]
        public string? Message { get; set; } = default;

        [MaxLength(1000)] 
        public string? StackTrace { get; set; } = default;
    }
}
