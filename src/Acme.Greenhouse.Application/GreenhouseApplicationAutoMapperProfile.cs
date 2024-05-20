using Acme.Greenhouse.DbLog;
using Acme.Greenhouse.Devices;
using Acme.Greenhouse.DeviceStatus;
using Acme.Greenhouse.Nodes;
using Acme.Greenhouse.NodeStatus;
using Acme.Greenhouse.SensorData;
using Acme.Greenhouse.Sensors;
using AutoMapper;

namespace Acme.Greenhouse;

public class GreenhouseApplicationAutoMapperProfile : Profile
{
    public GreenhouseApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Node, NodeDto>();
        CreateMap<NodeCreateUpdateDto, Node>();
        CreateMap<Sensor, SensorDto>();
        CreateMap<SensorCreateUpdateDto, Sensor>();
        CreateMap<Device, DeviceDto>();
        CreateMap<DeviceCreateUpdateDto, Device>();
        CreateMap<NodeStatus.NodeStatus, NodeStatusDto>();
        CreateMap<NodeStatusCreateDto, NodeStatus.NodeStatus>();
        CreateMap<SensorData.SensorData, SensorDataDto>();
        CreateMap<SensorDataCreateDto, SensorData.SensorData>();
        CreateMap<DeviceStatus.DeviceStatus, DeviceStatusDto>();
        CreateMap<DeviceStatusCreateDto, DeviceStatus.DeviceStatus>();
        CreateMap<DbLog.DbLog, DbLogDto>();
        CreateMap<DbLogCreateDto, DbLog.DbLog>();
    }
}
