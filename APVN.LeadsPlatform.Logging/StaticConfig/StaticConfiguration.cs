using System.Collections.Generic;

namespace APVN.LeadsPlatform.Logging.StaticConfig
{
    public static class StaticConfiguration
    {
        public static string KafkaServer { get; set; } = "2.2.2.83:57909";
        public static Dictionary<int, string> ApplicationStore = new Dictionary<int, string>();
    }
}
