using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Enums
{
    public enum CacheType
    {
        /// <summary>
        /// The no cache
        /// </summary>
        NoCache = 0,

        /// <summary>
        /// The redis
        /// </summary>
        Redis = 1,

        /// <summary>
        /// The memcache
        /// </summary>
        Memcache = 2,

        /// <summary>
        /// The IIS
        /// </summary>
        IIS = 3
    }
}
