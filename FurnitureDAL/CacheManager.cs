using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDAL
{
    public class CacheManager
    {
        private static readonly ObjectCache Cache = MemoryCache.Default;

        public static void SetCache(string key, string value, int durationInMilliseconds)
        {
            if (string.IsNullOrEmpty(key) || value == null)
                throw new ArgumentNullException("Cache key or value cannot be null.");

            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMilliseconds(durationInMilliseconds)
            };
            
            Cache.Set(key, value, policy);
        }

        public static string GetCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("Cache key cannot be null.");

            return Cache[key] as string; // Returns null if the key doesn't exist in the cache
        }

    }
}
