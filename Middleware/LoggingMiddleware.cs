using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            
            // Registrar la solicitud entrante
            _logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            await _next(context); // Pasar al siguiente middleware

            stopwatch.Stop();
            // Registrar la respuesta y el tiempo
            _logger.LogInformation($"Outgoing Response: {context.Response.StatusCode} - Time Taken: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}