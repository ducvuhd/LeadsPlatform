using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DVG.Autoportal.Core.Logging.LoggingMiddlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly Stopwatch _stopwatch;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("HttpContext");
            _stopwatch = new Stopwatch();
        }

        public async Task Invoke(HttpContext context)
        {
            _stopwatch.Restart();

            await _next(context).ConfigureAwait(false);
            _stopwatch.Stop();
            //loggedResponse = new LoggedResponse(context.Response);
            //var loggedContext = new CompletedContext(loggedResponse, stopWatch.ElapsedMilliseconds);
            _logger.LogInformation(_stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}