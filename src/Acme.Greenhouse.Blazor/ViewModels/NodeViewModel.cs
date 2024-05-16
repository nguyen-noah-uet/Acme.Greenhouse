using Acme.Greenhouse.Devices;
using Acme.Greenhouse.Nodes;
using Acme.Greenhouse.SensorData;
using Acme.Greenhouse.Sensors;
using System.Collections.Generic;

namespace Acme.Greenhouse.Blazor.ViewModels
{
    public class NodeViewModel
    {
        public NodeDto Node { get; set; } = default!;
        public bool IsOnline { get; set; }
        public List<SensorViewModel>? Sensors { get; set; }
        public List<DeviceViewModel>? Devices { get; set; }
        public NodeViewModel()
        {
        }
    }
    public class DeviceViewModel
    {
        public DeviceDto Device { get; set; } = default!;
        public bool IsOn { get; set; }
    }
    public class SensorViewModel
    {
        public SensorDto Sensor { get; set; }
        public double CurrentValue { get; set; } = double.NaN;
        public List<SensorDataDto>? Data { get; set; } = default;
    }
}
