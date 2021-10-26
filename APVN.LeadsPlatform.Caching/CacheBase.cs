using APVN.LeadsPlatform.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace APVN.LeadsPlatform.Caching
{
    public class CacheBase
    {
        HttpContext context;
        public CacheBase()
        {

        }

        public CacheBase(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor.HttpContext;
        }

        bool? _isRefreshCache;
        protected bool IsRefreshCache
        {
            get
            {
                if (_isRefreshCache != null) return _isRefreshCache.Value;
                if (context == null || context.Request == null)
                {
                    _isRefreshCache = false;
                    return _isRefreshCache.Value;
                }
                if (context.Request.Headers["X-Refresh-Cache"] == AppKeys.RefreshCacheKey)
                {
                    _isRefreshCache = true;
                    return _isRefreshCache.Value;
                }

                _isRefreshCache = false;
                return _isRefreshCache.Value;
            }
        }

        protected byte[] Serialize<T>(T item)
        {
            if (item == null || item.Equals(default(T)))
                return null;

            var bf = new BinaryFormatter();

            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, item);
                bytes = ms.ToArray();
            }

            return bytes;
        }

        protected T Desizalize<T>(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0)
                return default(T);

            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                var obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
