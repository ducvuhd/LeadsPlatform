using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public class AppKeys
    {
        public static string Domain = AppSettings.GetAppKey("Domain");
        public static string JWTSecretKey = AppSettings.GetAppKey("JWTSecretKey");
        public static string JWTIssuer = AppSettings.GetAppKey("JWTIssuer");
        public static string JWTAudience = AppSettings.GetAppKey("JWTAudience");
        public static int JWTTimeout = AppSettings.GetAppKey<int>("JWTTimeout");
        public static string PasswordSalt = AppSettings.GetAppKey("PasswordSalt");
        //public static List<string> RoleExcludedUrls = AppSettings.GetList("RoleExcludedUrls");
        // Cache config
        public static string RefreshCacheKey = AppSettings.Get("RefreshCacheKey");
        public static int CacheType = AppSettings.GetAppKey<int>("CacheType");
    }
}
