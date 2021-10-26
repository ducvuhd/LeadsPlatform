using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.Caching
{
    public interface ICache
    {
        /// <summary>
        /// Check key exist in cache
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:33 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Exists(string key);
        /// <summary>
        /// Get string key from cache
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:33 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Get(string key);
        /// <summary>
        /// The get boolean
        /// Author: ThanhDT
        /// Created date: 9/21/2020 2:37 PM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool GetBoolean(string key);
        /// <summary>
        /// The get generic
        /// Author: ThanhDT
        /// Created date: 9/21/2020 11:57 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetGeneric<T>(string key, T defaultValue = default) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>;
        /// <summary>
        /// Get object key from cache
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:34 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T>(string key) where T : new();
        /// <summary>
        /// Set string key to cache with no expired
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:34 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        bool Set(string key, string item);
        /// <summary>
        /// Set string key to cache with expired
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:34 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        bool Set(string key, string item, int expireInSeconds);
        /// <summary>
        /// The set generic
        /// Author: ThanhDT
        /// Created date: 9/21/2020 11:57 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        bool SetGeneric<T>(string key, T item, int expireInSeconds) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>;
        /// <summary>
        /// Set object key to cache with no expired
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:35 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        bool Set<T>(string key, T item) where T : new();
        /// <summary>
        /// Set string key to cache with expired
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:35 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <param name="expireInSeconds">The expire in seconds.</param>
        /// <returns></returns>
        bool Set<T>(string key, T item, int expireInSeconds) where T : new();
        /// <summary>
        /// The list left push
        /// Author: ThanhDT
        /// Created date: 9/29/2020 10:21 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        long ListLeftPush(string key, string item);
        /// <summary>
        /// Delete key from cache
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:35 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Delete(string key);
        /// <summary>
        /// Delete pattern
        /// Author     : ThanhDT
        /// Create Date: 11/16/2020 6:23 PM
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        bool DeletePattern(string prefix);
        /// <summary>
        /// Get time to live of a key cache
        /// Author: ThanhDT
        /// Created date: 9/19/2020 11:36 AM
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        int TimeToLive(string key);

        #region Asynchonous
        Task<string> GetAsync(string key);
        Task<T> GetAsync<T>(string key);
        Task<bool> SetAsync(string key, string item);
        Task<bool> SetAsync(string key, string item, int expireInSeconds);
        Task<bool> SetAsync<T>(string key, T item);
        Task<bool> SetAsync<T>(string key, T item, int expireInSeconds);
        #endregion
    }
}
