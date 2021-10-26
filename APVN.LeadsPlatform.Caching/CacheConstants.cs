using APVN.LeadsPlatform.Utility;
using System;

namespace APVN.LeadsPlatform.Caching
{
    public class CacheConstants
    {
        public static int ExpireShortTime = AppSettings.Get<int>("Cache:Expire:ShortTime");
        public static int ExpireMediumTime = AppSettings.Get<int>("Cache:Expire:MediumTime");
        public static int ExpireLongTime = AppSettings.Get<int>("Cache:Expire:LongTime");
        public static int ExpireOneWeek = AppSettings.Get<int>("Cache:Expire:OneWeek");

        /// JWT token cache
        public const string JWTToken = "JWTToken:{0}";
        public const string ObsoleteJWTToken = "ObsoleteJWTToken:{0}:{1}";
        public const string JWTLockRefreshToken = "JWTLockRefreshToken:{0}";
        public const string UserRoleToken = "{0}:Roles";

        // Make cache
        public const string LeadMakeCache = "LeadMakeCache";
        public const string LeadModelCache = "LeadModelCache";
        public const string LeadCityCache = "LeadCityCache";
        public const string LeadUserCache = "LeadUserCache";
    }
}
