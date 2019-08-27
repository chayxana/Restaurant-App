using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Identity.API.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory)
        {
            var _logger = loggerFactory.CreateLogger("init");
            // Request method, scheme, and path
            _logger.LogDebug("Request Method: {METHOD}", context.Request.Method);
            _logger.LogDebug("Request Scheme: {SCHEME}", context.Request.Scheme);
            _logger.LogDebug("Request Path: {PATH}", context.Request.Path);
            _logger.LogDebug("Base Path: {PATHBASE}", context.Request.PathBase);
            _logger.LogDebug("Host: {HOST}", context.Request.Host.Value);

            // Headers
            foreach (var header in context.Request.Headers)
            {
                _logger.LogDebug("Header: {KEY}: {VALUE}", header.Key, header.Value);
            }

            // Connection: RemoteIp
            _logger.LogDebug("Request RemoteIp: {REMOTE_IP_ADDRESS}", context.Connection.RemoteIpAddress);
            await _next(context);
        }
    }
}