using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Greenhouse.Blazor.Middlewares
{
    public class IPAddressMiddleware(IHttpContextAccessor _httpContextAccessor, ILogger<IPAddressMiddleware> _logger) : IMiddleware
    {
        private Dictionary<string, long> _ipDictionary = new Dictionary<string, long>();
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Get client IP address
            string? clientIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            if(clientIp != null) {
                if (_ipDictionary.ContainsKey(clientIp)) {
                    _ipDictionary[clientIp] += 1;
                }
                else
                {
                    _ipDictionary[clientIp] = 1;
                }
                _logger.LogInformation("{ip} {count}", clientIp, _ipDictionary[clientIp]);
            }
            await next(context);
        }
    }
}
