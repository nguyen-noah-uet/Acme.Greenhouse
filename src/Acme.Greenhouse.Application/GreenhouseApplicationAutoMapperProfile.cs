using Acme.Greenhouse.Nodes;
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
        CreateMap<NodeStatus, NodeStatusDto>();
        CreateMap<NodeStatusCreateDto, NodeStatus>();
        CreateMap<SensorData, SensorDataDto>();
        CreateMap<SensorDataCreateDto, SensorData>();
        CreateMap<DeviceStatus, DeviceStatusDto>();
        CreateMap<DeviceStatusCreateDto, DeviceStatus>();
        CreateMap<DbLog, DbLogDto>();
        CreateMap<DbLogCreateDto, DbLog>();
    }
}
