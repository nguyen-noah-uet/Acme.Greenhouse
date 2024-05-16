using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Microsoft.Extensions.Logging;

namespace Acme.Greenhouse.DbLog
{
    public class DbLog : CreationAuditedEntity<int>
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        [MaxLength(1000)]
        public string? Message { get; set; } = default;

        [MaxLength(1000)]
        public string? StackTrace { get; set; } = default;
    }
}
