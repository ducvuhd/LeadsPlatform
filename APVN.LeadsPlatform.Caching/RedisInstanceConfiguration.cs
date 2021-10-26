using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Caching
{
    public class RedisInstanceConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public int Database { get; set; }
        public int Timeout { get; set; }
        public string AuthName { get; set; }
        public string AuthPassword { get; set; }
        public string SlotNameInMemory { get; set; }
        public string Name { get; set; }
        public bool AllowCache { get; set; }
    }
}
