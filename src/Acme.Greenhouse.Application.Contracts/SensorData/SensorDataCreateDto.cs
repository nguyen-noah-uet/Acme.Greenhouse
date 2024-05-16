using System;

namespace Acme.Greenhouse.SensorData
{
    [Serializable]
    public class SensorDataCreateDto
    {
        public int SensorId { get; set; }
        public double Value { get; set; }
    }
}
