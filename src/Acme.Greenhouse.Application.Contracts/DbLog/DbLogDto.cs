using System;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;

namespace Acme.Greenhouse.DbLog
{
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
