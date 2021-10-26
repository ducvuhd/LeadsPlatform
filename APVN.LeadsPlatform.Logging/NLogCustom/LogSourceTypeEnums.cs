using System;
using System.ComponentModel;

namespace APVN.LeadsPlatform.Logging.NLogCustom
{
    public enum LogSourceTypeEnums
    {

        [Description("[APVN] LeadsPlatform")]
        LeadsPlatform = 1020
    }

    public static class EnumConvert
    {
        public static string GetEnumDescription(Enum value)
        {
            try
            {
                var fi = value.GetType().GetField(value.ToString());

                var attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);
                return attributes?.Length > 0 ? attributes[0].Description : value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
