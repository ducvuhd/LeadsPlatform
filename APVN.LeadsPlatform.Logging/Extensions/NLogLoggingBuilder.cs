using APVN.LeadsPlatform.Logging.NLogCustom;
using APVN.LeadsPlatform.Logging.StaticConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.Collections.Generic;

namespace APVN.LeadsPlatform.Logging.Extensions
{
    public static class NLogLoggingBuilder
    {
        public static void UseSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            //builder.AddSerilog();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration, "Logging:Providers:Serilog")
            //    .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.Logger(configuration =>
            //    {
            //        configuration.WriteTo.File("Logs/error.log");
            //        configuration.Filter.ByIncludingOnly(ev => ev.)
            //    })
            //    .CreateLogger();
        }

        /// <summary>
        /// The use nlog
        /// Author: ToanNQ
        /// Created date: 9/15/2020 6:58 PM
        /// </summary>
        /// <param name="configFile">The configuration file.</param>
        /// <param name="kafkaTaget">The kafka taget.</param>
        /// <param name="applicationStore">The application store.</param>
        public static void UseNLog(string configFile, string kafkaTaget, LogSourceTypeEnums applicationStore)
        {
            NLogTargetCustom.RegisterTarget();
            StaticConfiguration.KafkaServer = kafkaTaget;
            StaticConfiguration.ApplicationStore = new Dictionary<int, string> { { (int)applicationStore, (EnumConvert.GetEnumDescription(applicationStore)) } };
            NLogBuilder.ConfigureNLog(configFile);
        }
    }
}