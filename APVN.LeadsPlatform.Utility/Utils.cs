using APVN.LeadsPlatform.Utility.AttributeCustoms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public static class Utils
    {
        const string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        const string koDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        public static long ConvertToUnixTime(DateTime dateTime)
        {
            var datetimeOffset = (DateTimeOffset)TimeZoneInfo.ConvertTimeToUtc(dateTime);
            long unixTime = datetimeOffset.ToUnixTimeMilliseconds();

            return unixTime;
        }

        public static DateTime ConvertFromUnixTime(long unixTime)
        {
            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(unixTime);

            return dateTimeOffset.UtcDateTime;
        }

        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi?.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static KeyValuePair<string, string> GetBitwiseEnumDescription<T>(int value)
        {
            try
            {
                string desReturn = "";
                string keyReturn = "";
                T[] array = (T[])Enum.GetValues(typeof(T));
                List<T> list = new List<T>(array);
                foreach (var item in list)
                {
                    var descriptions = GetEnumDescription((Enum)Enum.Parse(typeof(T), item.ToString(), true));
                    if (HasState(value, item.GetHashCode()))
                    {
                        desReturn += descriptions + ", ";
                        keyReturn += item.GetHashCode() + ", ";
                    }
                }
                return new KeyValuePair<string, string>(keyReturn.Trim().Trim(','), desReturn.Trim().Trim(','));
            }
            catch (Exception ex)
            {
                return new KeyValuePair<string, string>();
            }
        }
        public static List<KeyValuePair<int, string>> GetAllEnumValueAndDescription<T>()
        {
            T[] array = (T[])Enum.GetValues(typeof(T));
            List<T> list = new List<T>(array);
            var lstValueAndDescription = new List<KeyValuePair<int, string>>();
            foreach (var item in list)
            {
                //var description = Enum.GetName(typeof(T), item);
                var descriptions = GetEnumDescription((Enum)Enum.Parse(typeof(T), item.ToString(), true));
                lstValueAndDescription.Add(new KeyValuePair<int, string>(item.GetHashCode(), descriptions));
            }
            return lstValueAndDescription;
        }
        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
        /// <summary>
        /// So sánh Object có thay đổi gì không so với trước truyền class chứa properties không muốn so sánh
        /// </summary>
        /// <typeparam name="T">Kiểu Object</typeparam>
        /// <param name="oldObject">Object cũ</param>
        /// <param name="newObject">Object sau khi thay đổi</param>
        /// <param name="fieldNameChanged">Tên các trường thay đổi</param>
        /// <param name="ignore">Các trường không muốn so sánh</param>
        /// <returns>Bool</returns>
        public static bool EqualObject<T, TIgnore, TEquals>(T oldObject, T newObject, TIgnore ignore, out List<TEquals> lstEqual) where T : class where TIgnore : class where TEquals : class, new()
        {
            string fieldNameChanged = string.Empty;
            lstEqual = new List<TEquals>();
            if (oldObject != null && newObject != null)
            {
                Type type = typeof(T);
                Type ignoreType = typeof(TIgnore);
                Type equalType = typeof(TEquals);
                List<string> ignoreList = ignoreType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Select(x => x.Name).ToList();
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        string descriptionField = "";
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])pi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                            descriptionField = attributes[0].Description;
                        else
                            descriptionField = pi.Name;

                        object oldValue = type.GetProperty(pi.Name).GetValue(oldObject, null);
                        object newValue = type.GetProperty(pi.Name).GetValue(newObject, null);
                        ReturnValueFieldAttribute[] customAttr = (ReturnValueFieldAttribute[])pi.GetCustomAttributes(typeof(ReturnValueFieldAttribute), false);
                        if (customAttr != null && customAttr.Length > 0)
                        {
                            oldValue = type.GetProperty(customAttr[0].Field).GetValue(oldObject, null);
                            newValue = type.GetProperty(customAttr[0].Field).GetValue(newObject, null);
                        }
                        if (oldValue != newValue && (oldValue == null || (newValue != null && !oldValue.Equals(newValue))))
                        {
                            TEquals equals = new TEquals();
                            equalType.GetProperty("FieldName").SetValue(equals, descriptionField);
                            equalType.GetProperty("OldValue").SetValue(equals, oldValue?.ToString());
                            equalType.GetProperty("NewValue").SetValue(equals, newValue?.ToString());
                            lstEqual.Add(equals);
                            fieldNameChanged += pi.Name + ',';
                        }
                    }
                }
                return fieldNameChanged == string.Empty;
            }
            return oldObject == newObject;
        }
        /// <summary>
        /// So sánh Object có thay đổi gì không so với trước truyền chuỗi string k muốn so sánh
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TEquals"></typeparam>
        /// <param name="ignoreList">Chuỗi string không muốn so sánh</param>
        /// <returns></returns>
        public static bool EqualObject<T, TEquals>(T oldObject, T newObject, out List<TEquals> lstEqual, params string[] ignoreList) where T : class where TEquals : class, new()
        {
            string fieldNameChanged = string.Empty;
            lstEqual = new List<TEquals>();
            Type equalType = typeof(TEquals);
            if (oldObject != null && newObject != null)
            {
                Type type = typeof(T);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        string descriptionField = "";
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])pi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (attributes != null && attributes.Length > 0)
                            descriptionField = attributes[0].Description;
                        else
                            descriptionField = pi.Name;

                        object oldValue = type.GetProperty(pi.Name).GetValue(oldObject, null);
                        object newValue = type.GetProperty(pi.Name).GetValue(newObject, null);
                        ReturnValueFieldAttribute[] customAttr = (ReturnValueFieldAttribute[])pi.GetCustomAttributes(typeof(ReturnValueFieldAttribute), false);
                        if (customAttr != null && customAttr.Length > 0)
                        {
                            oldValue = type.GetProperty(customAttr[0].Field).GetValue(oldObject, null);
                            newValue = type.GetProperty(customAttr[0].Field).GetValue(newObject, null);
                        }

                        if (oldValue != newValue && (oldValue == null || (newValue != null && !oldValue.Equals(newValue))))
                        {
                            TEquals equals = new TEquals();
                            equalType.GetProperty("FieldName").SetValue(equals, descriptionField);
                            equalType.GetProperty("OldValue").SetValue(equals, oldValue);
                            equalType.GetProperty("NewValue").SetValue(equals, newValue);
                            lstEqual.Add(equals);
                            fieldNameChanged += pi.Name + ',';
                        }
                    }
                }
                return fieldNameChanged == string.Empty;
            }
            return oldObject == newObject;
        }

        public static string UnicodeToKoDauAndSpace(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string UnicodeToKoDauAndGach(string s)
        {
            string strChar = "abcdefghijklmnopqrstxyzuvxw0123456789 -";
            //string retVal = UnicodeToKoDau(s);
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
                        sReturn += "-";
                }
            }

            return sReturn;
        }
        public static int ToInt(this object obj, int? defaultValue)
        {
            int ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt32(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>long</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static long ToLong(this object obj, long? defaultValue)
        {
            long ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt64(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>double</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double? defaultValue)
        {
            Double ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToDouble(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float? defaultValue)
        {
            float ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToSingle(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj, bool? defaultValue)
        {
            bool ret = defaultValue ?? false;
            try
            {
                ret = Convert.ToBoolean(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        public static DateTime ToDateTime(this object obj, DateTime? defaultValue)
        {
            DateTime ret = defaultValue ?? DateTime.MinValue;
            try
            {
                ret = Convert.ToDateTime(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        public static int TurnOn(this int s, int b) { return s | b; }
        public static int TurnOff(this int s, int b) { return s & ~b; }

        public static bool HasState(this int s, int b)
        {
            return (s & b) == b;
        }
        public static bool HasState(this long s, long b)
        {
            return (s & b) == b;
        }
        public static long Keep(this long s, params long[] b) { return s & (long)b.Cast<long>().Sum(); }
        public static bool SingleState(this long s)
        {
            for (int i = 1; i <= 45; i++)
            {
                if (((long)1 << i) == s)
                {
                    return true;
                }
            }
            return false;
        }
        public static long Keep(this int s, params int[] b) { return s & (int)b.Cast<int>().Sum(); }
        public static bool SingleState(this int s)
        {
            for (int i = 1; i <= 45; i++)
            {
                if (((int)1 << i) == s)
                {
                    return true;
                }
            }
            return false;
        }
        public static string RandomString(int size)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
