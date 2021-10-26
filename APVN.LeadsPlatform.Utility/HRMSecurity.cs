using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public class HRMSecurity
    {
        private const string KeyFormat = "dvs.it.department.{0:yyyyMMdd}";

        /// <summary>
        /// Create Security Key
        /// </summary>
        /// <returns></returns>
        public static string CreateKey()
        {
            var key = CryptUtils.MD5Hash(string.Format(KeyFormat, DateTime.Now));
            return key;
        }

        /// <summary>
        /// Check Security Key
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool IsSecretKey(string secretKey)
        {
            var key = CryptUtils.MD5Hash(string.Format(KeyFormat, DateTime.Now));
            if (key.Equals(secretKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
