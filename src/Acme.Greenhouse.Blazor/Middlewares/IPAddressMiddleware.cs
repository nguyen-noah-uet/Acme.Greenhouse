using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Acme.Greenhouse.Blazor.Middlewares
{
    public class IPAddressMiddleware(IHttpContextAccessor _httpContextAccessor, ILogger<IPAddressMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Get client IP address
            string? clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            if(clientIp != null) {
                _logger.LogInformation("{ip}", clientIp);
            }
            await next(context);
        }
    }
}
