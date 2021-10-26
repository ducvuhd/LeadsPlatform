using APVN.LeadsPlatform.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer.Cache
{
    public class CacheService
    {
        ICache _cache;

        public CacheService(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// The execute
        /// Author: ThanhDT
        /// Created date: 9/21/2020 12:01 PM
        /// </summary>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public string Execute(Func<string> func, string cacheKey)
        {
            return Execute(func, cacheKey, CacheConstants.ExpireMediumTime);
        }

        /// <summary>
        /// The execute
        /// Author: ThanhDT
        /// Created date: 9/21/2020 12:01 PM
        /// </summary>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        public string Execute(Func<string> func, string cacheKey, int expireInSeconds)
        {
            var value = _cache.Get(cacheKey);

            if (value == null)
            {
                value = func.Invoke();
                if (value != null)
                {
                    _cache.Set(cacheKey, value, expireInSeconds);
                }
            }

            return value;
        }

        /// <summary>
        /// The execute boolean
        /// Author: ThanhDT
        /// Created date: 9/21/2020 2:50 PM
        /// </summary>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        public bool ExecuteBoolean(Func<bool> func, string cacheKey, int expireInSeconds)
        {
            if (_cache.Exists(cacheKey))
            {
                return _cache.GetBoolean(cacheKey);
            }

            var result = func.Invoke();
            _cache.SetGeneric(cacheKey, result, expireInSeconds);

            return result;
        }

        /// <summary>
        /// The execute generic
        /// Author: ThanhDT
        /// Created date: 9/21/2020 12:02 PM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public T ExecuteGeneric<T>(Func<T> func, string cacheKey) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            return ExecuteGeneric(func, cacheKey, CacheConstants.ExpireMediumTime);
        }

        /// <summary>
        /// The execute generic
        /// Author: ThanhDT
        /// Created date: 9/21/2020 12:02 PM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        public T ExecuteGeneric<T>(Func<T> func, string cacheKey, int expireInSeconds) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            T obj = _cache.GetGeneric<T>(cacheKey);

            if (EqualityComparer<T>.Default.Equals(obj, default))
            {
                obj = func.Invoke();
                if (!EqualityComparer<T>.Default.Equals(obj, default))
                {
                    _cache.SetGeneric(cacheKey, obj, expireInSeconds);
                }
            }

            return obj;
        }

        /// <summary>
        /// The execute
        /// Author: ThanhDT
        /// Created date: 9/21/2020 9:43 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public T Execute<T>(Func<T> func, string cacheKey) where T : new()
        {
            return Execute(func, cacheKey, CacheConstants.ExpireMediumTime);
        }

        /// <summary>
        /// The execute
        /// Author: ThanhDT
        /// Created date: 9/21/2020 9:43 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        public T Execute<T>(Func<T> func, string cacheKey, int expireInSeconds) where T : new()
        {
            T obj = _cache.Get<T>(cacheKey);

            if (obj == null)
            {
                obj = func.Invoke();
                if (obj != null)
                {
                    _cache.Set(cacheKey, obj, expireInSeconds);
                }
            }

            return obj;
        }

        /// <summary>
        /// The delete
        /// Author: ThanhDT
        /// Created date: 9/21/2020 9:43 AM
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        public void Delete(string cacheKey)
        {
            _cache.Delete(cacheKey);
        }

        /// <summary>
        /// The format key
        /// Author: ThanhDT
        /// Created date: 9/21/2020 9:43 AM
        /// </summary>
        /// <param name="keyFormat">The key format.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public string FormatKey(string keyFormat, params object[] arguments)
        {
            return string.Format("{0}:{1}", RedisCache.CacheDataConfig.Name, string.Format(keyFormat, arguments));
        }
    }
}
