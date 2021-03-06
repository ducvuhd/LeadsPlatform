using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace DVG.Autoportal.Core.Logging.Extensions
{
    public static class IHostBuilderExtensions
    {
        public static IHostBuilder AddNLog(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseNLog();
        }
    }
}