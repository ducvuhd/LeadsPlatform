using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Utility
{
    public class AppSettings
    {
        public const string AppKeyFormat = "AppKeys:";
        private static AppSettings _instance;
        private static readonly object ObjLocked = new object();
        private IConfiguration _configuration;

        protected AppSettings()
        {
        }

        public void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static AppSettings Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (ObjLocked)
                    {
                        if (null == _instance)
                            _instance = new AppSettings();
                    }
                }
                return _instance;
            }
        }

        public string GetConnection(string key, string defaultValue = "")
        {
            try
            {
                return _configuration.GetConnectionString(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static string GetObject(string key = null)
        {
            var section = Instance._configuration.GetSection(key);
            return section.Value;
        }

        private static T GetObject<T>(string key = null)
        {
            var section = Instance._configuration.GetSection(key);
            return section.Get<T>();
        }

        private static T GetObject<T>(string key, T defaultValue)
        {
            if (Instance._configuration.GetSection(key) == null)
                return defaultValue;

            return Instance._configuration.GetSection(key).Get<T>();
        }

        public static string Get(string key = null)
        {
            try
            {
                return GetObject(key);
            }
            catch
            {
                return null;
            }
        }

        public static T Get<T>(string key = null)
        {
            try
            {
                return GetObject<T>(key);
            }
            catch
            {
                return default(T);
            }
        }

        public static T Get<T>(string key, T defaultValue)
        {
            try
            {
                return GetObject<T>(key, defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T GetAppKey<T>(string key = null)
        {
            var fullKey = AppKeyFormat + key;
            try
            {
                return GetObject<T>(fullKey);
            }
            catch
            {
                return default(T);
            }
        }

        public static string GetAppKey(string key = null)
        {
            var fullKey = AppKeyFormat + key;
            try
            {
                return GetObject(fullKey);
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetList(string key = null)
        {
            var fullKey = AppKeyFormat + key;
            try
            {
                var value = GetObject(fullKey);
                return value.Split(';').ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public string GetString(string key, string defaultValue = "")
        {
            try
            {
                return _configuration.GetSection("StringValue").GetChildren().FirstOrDefault(x => x.Key == key)?.Value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
