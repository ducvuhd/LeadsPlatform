using APVN.LeadsPlatform.Utility;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.Caching
{
    public class RedisCache : CacheBase, ICache
    {
        public static RedisInstanceConfiguration CacheDataConfig = AppSettings.Get<RedisInstanceConfiguration>("Cache:Redis:Data");
        protected IDatabase _cache;
        private bool _allowCache;
        private IConnectionMultiplexer _connectionMultiplexer;

        public RedisCache()
        {
            InitRedis(CacheDataConfig);
        }

        public RedisCache(RedisInstanceConfiguration redisConfig)
        {
            InitRedis(redisConfig);
        }

        private void InitRedis(RedisInstanceConfiguration redisConfig)
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { { redisConfig.Server, redisConfig.Port } },
                DefaultDatabase = redisConfig.Database,
                ConnectTimeout = redisConfig.Timeout,
                AsyncTimeout = redisConfig.Timeout / 2,
                ClientName = redisConfig.Name,
                AbortOnConnectFail = false,
                ConnectRetry = 3,
                KeepAlive = 20
            };
            _allowCache = redisConfig.AllowCache;
            try
            {
                _connectionMultiplexer = ConnectionMultiplexer.Connect(config);
                _cache = _connectionMultiplexer.GetDatabase(redisConfig.Database);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
            }
        }

        public bool Exists(string key)
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.KeyExists(key);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public string Get(string key)
        {
            if (_cache == null || !_allowCache) return null;
            try
            {
                return _cache.StringGet(key);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        public bool GetBoolean(string key)
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return (bool)_cache.StringGet(key);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public T GetGeneric<T>(string key, T defaultValue = default) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            if (_cache == null || !_allowCache) return default;
            try
            {
                var redisValue = (string)_cache.StringGet(key);
                return redisValue != null ? (T)Convert.ChangeType(redisValue, typeof(T)) : defaultValue;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return default;
            }
        }

        public T Get<T>(string key) where T : new()
        {
            if (_cache == null || !_allowCache) return default;
            try
            {
                byte[] bytValue = _cache.StringGet(key);
                return Desizalize<T>(bytValue);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return default(T);
            }
        }

        public bool Set(string key, string item)
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.StringSet(key, item);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public bool Set(string key, string item, int expireInSeconds)
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.StringSet(key, item, new TimeSpan(0, 0, expireInSeconds));
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public bool SetGeneric<T>(string key, T item, int expireInSeconds) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                var value = RedisValue.Unbox(item);
                return _cache.StringSet(key, value, new TimeSpan(0, 0, expireInSeconds));
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public bool Set<T>(string key, T item) where T : new()
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.StringSet(key, Serialize(item));
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public bool Set<T>(string key, T item, int expireInSeconds) where T : new()
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.StringSet(key, Serialize(item), new TimeSpan(0, 0, expireInSeconds));
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public long ListLeftPush(string key, string item)
        {
            if (_cache == null || !_allowCache) return 0;
            try
            {
                return _cache.ListLeftPush(key, item);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return 0;
            }
        }

        public bool Delete(string key)
        {
            if (_cache == null || !_allowCache) return false;
            try
            {
                return _cache.KeyDelete(key);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public int TimeToLive(string key)
        {
            if (_cache == null || !_allowCache) return 0;
            try
            {
                var timespan = _cache.KeyTimeToLive(key);
                return timespan != null ? Convert.ToInt32(timespan.Value.TotalSeconds) : 0;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return -2;
            }
        }

        public bool DeletePattern(string keyPattern)
        {
            try
            {
                var endPoints = _cache.Multiplexer.GetEndPoints();
                if (endPoints != null && endPoints.Length != 0)
                {
                    var keysList = _connectionMultiplexer.GetServer(endPoints[0]).Keys(_cache.Database, keyPattern);
                    if (keysList != null)
                    {
                        var arrKeys = keysList.ToArray();
                        _cache.KeyDeleteAsync(arrKeys);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }

            return true;
        }

        #region Asynchonous

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, string item, int expireInSeconds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync<T>(string key, T item, int expireInSeconds)
        {
            throw new NotImplementedException();
        }

        Task<string> ICache.GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        Task<T> ICache.GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICache.SetAsync(string key, string item)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICache.SetAsync(string key, string item, int expireInSeconds)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICache.SetAsync<T>(string key, T item)
        {
            throw new NotImplementedException();
        }

        Task<bool> ICache.SetAsync<T>(string key, T item, int expireInSeconds)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
