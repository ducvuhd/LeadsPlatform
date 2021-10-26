using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public static class Extensions
    {
        public static T To<T>(this string input, T defaultValue = default)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;

            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return defaultValue;
            }
        }

        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.Where(x => x.Type == claimType).FirstOrDefault();

            return claim == null ? string.Empty : claim.Value;
        }
    }
}
