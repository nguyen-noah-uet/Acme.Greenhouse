using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Sensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.Greenhouse.Nodes
{
    public interface INodeService
        : ICrudAppService<NodeDto, int, PagedAndSortedResultRequestDto, NodeCreateUpdateDto, NodeCreateUpdateDto>
    {
        //Task<List<NodeDto>> GetListDetailedAsync();
    }

    [Serializable]
    public class NodeCreateUpdateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(200)]
        public string? Description { get; set; }
    }

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

    
    public interface IDeviceService: 
        ICrudAppService<DeviceDto, int, PagedAndSortedResultRequestDto, DeviceCreateUpdateDto, DeviceCreateUpdateDto>
    {
        Task<List<DeviceDto>> GetByNodeId(int nodeId);
    }

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

    [Serializable]
    public class DeviceDto: AuditedEntityDto<int>
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

    public interface ISensorService:
        ICrudAppService<SensorDto, int, PagedAndSortedResultRequestDto, SensorCreateUpdateDto, SensorCreateUpdateDto>
    {
        Task<List<SensorDto>> GetByNodeId(int nodeId);
    }

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

    [Serializable]
    public class SensorDto : AuditedEntityDto<int>
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public int NodeId { get; set; }
        public double HighThreshold { get; set; }
        public double LowThreshold { get; set; }
        public SensorType SensorType { get; set; }
        public string? Unit { get; set; }
        public override string ToString()
        {
            return $"Sensor [Id={Id},Name={Name}]";
        }
    }

    public interface ISensorDataService : ICrudAppService<SensorDataDto, int, PagedAndSortedResultRequestDto, SensorDataCreateDto>
    {
        Task<List<SensorDataDto>> GetByDate(int sensorId, DateTime from, DateTime to);
    }

    [Serializable]
    public class SensorDataDto : CreationAuditedEntityDto<int>
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
    }

    [Serializable]
    public class SensorDataCreateDto
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
    }
    
    public interface INodeStatusService : ICrudAppService<NodeStatusDto, int, PagedAndSortedResultRequestDto, NodeStatusCreateDto>
    {
    }

    [Serializable]
    public class NodeStatusCreateDto
    {
        public int NodeId { get; set; } = default!;
        public bool IsOnline { get; set; }
    }

    [Serializable]
    public class NodeStatusDto : CreationAuditedEntityDto<int>
    {
        public int NodeId { get; set; } = default!;
        public bool IsOnline { get; set; }
    }

    public interface IDeviceStatusService : ICrudAppService<DeviceStatusDto, int, PagedAndSortedResultRequestDto, DeviceStatusCreateDto>
    {
    }

    [Serializable]
    public class DeviceStatusCreateDto
    {
        public int DeviceId { get; set; } = default!;
        public bool IsOn { get; set; }
    }

    [Serializable]
    public class DeviceStatusDto : CreationAuditedEntityDto<int>
    {
        public int DeviceId { get; set; } = default!;
        public bool IsOn { get; set; }
    }

    public interface IDbLogService : ICrudAppService<DbLogDto, int, PagedAndSortedResultRequestDto, DbLogCreateDto>
    {
    }
    [Serializable]
    public class DbLogCreateDto
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public string? Message { get; set; } = default;
        public string? StackTrace { get; set; } = default;
    }

    [Serializable]
    public class DbLogDto : CreationAuditedEntityDto<int>
    {
        public LogLevel LogLevel { get; set; }
        public string? Message { get; set; } = default;
        public string? StackTrace { get; set; } = default;
        public override string ToString()
        {
            return $"[{CreationTime:yyyy-MM-dd HH:mm:ss} {LogLevel.ToString().ToUpper()}] {Message} {StackTrace}";
        }
    }
}
