using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.Caching
{
    public class NoCache : ICache
    {
        public bool Delete(string key)
        {
            return false;
        }

        public bool DeletePattern(string prefix)
        {
            return false;
        }

        public bool Exists(string key)
        {
            return false;
        }

        public string Get(string key)
        {
            return null;
        }

        public T Get<T>(string key) where T : new()
        {
            return default(T);
        }

        public Task<string> GetAsync(string key)
        {
            return Task.FromResult<string>(null);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(default(T));
        }

        public bool GetBoolean(string key)
        {
            throw new NotImplementedException();
        }

        public T GetGeneric<T>(string key, T defaultValue) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            return defaultValue;
        }

        public long ListLeftPush(string key, string item)
        {
            return 0;
        }

        public bool Set(string key, string item)
        {
            return false;
        }

        public bool Set(string key, string item, int expireInSeconds)
        {
            return false;
        }

        public bool Set<T>(string key, T item) where T : new()
        {
            return false;
        }

        public bool Set<T>(string key, T item, int expireInSeconds) where T : new()
        {
            return false;
        }

        public Task<bool> SetAsync(string key, string item)
        {
            return Task.FromResult(false);
        }

        public Task<bool> SetAsync(string key, string item, int expireInSeconds)
        {
            return Task.FromResult(false);
        }

        public Task<bool> SetAsync<T>(string key, T item)
        {
            return Task.FromResult(false);
        }

        public Task<bool> SetAsync<T>(string key, T item, int expireInSeconds)
        {
            return Task.FromResult(false);
        }

        public bool SetGeneric<T>(string key, T item, int expireInSeconds) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            return false;
        }

        public int TimeToLive(string key)
        {
            return 0;
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
    }
}
