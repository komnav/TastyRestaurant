using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Restaurant.WebApi.Middleware
{
    public class EfficientStopwatch
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EfficientStopwatch> _logger;
        public EfficientStopwatch(RequestDelegate next, ILogger<EfficientStopwatch> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = Stopwatch.GetTimestamp();

            await _next(context);

            var endTime = Stopwatch.GetTimestamp();

            var diff = Stopwatch.GetElapsedTime(startTime, endTime);
            _logger.LogInformation($" Code Execution Time: {diff}");
        }

    }
}
