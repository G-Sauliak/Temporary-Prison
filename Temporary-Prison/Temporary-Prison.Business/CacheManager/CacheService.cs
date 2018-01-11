﻿using System;
using System.Web;
using System.Web.Caching;

namespace Temporary_Prison.Business.CacheManager
{
    public class CacheService : ICacheService
    {
        private const int cacheDefaultTimeoutInMinute = 30;

        public TResult GetOrSet<TResult>(string cacheKey, Func<TResult> getCallback) where TResult : class
        {
            var item = default(TResult);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                item = HttpRuntime.Cache.Get(cacheKey) as TResult;
                return item;
            }
            item = getCallback();
            if (item != null)
            {
                HttpRuntime.Cache.Insert(cacheKey, item, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheDefaultTimeoutInMinute));
                return item;
            }
            return default(TResult);
        }

        public TResult GetOrSet<TResult>(string cacheKey, Func<TResult> getCallback, TimeSpan expirationTime) where TResult : class
        {
            var item = default(TResult);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                item = HttpRuntime.Cache.Get(cacheKey) as TResult;
                return item;
            }
            item = getCallback();
            if (item != null)
            {
                HttpRuntime.Cache.Insert(cacheKey, item, null, Cache.NoAbsoluteExpiration, expirationTime);
                return item;
            }
            return default(TResult);
        }

        public void Remove(string cacheKey)
        {
            HttpRuntime.Cache.Remove(cacheKey);
        }
    }
}
