using System;
using Microsoft.Extensions.Logging;

namespace Acme.Greenhouse.DbLog
{
    [Serializable]
    public class DbLogCreateDto
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public string? Message { get; set; } = default;
        public string? StackTrace { get; set; } = default;
    }
}
