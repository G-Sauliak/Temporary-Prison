using System;

namespace Temporary_Prison.Business.CacheManager
{
    public interface ICacheService
    {
        TResult GetOrSet<TResult>(string cacheKey, Func<TResult> getCallback) where TResult : class;
        TResult GetOrSet<TResult>(string cacheKey, Func<TResult> getCallback, DateTime expirationTime) where TResult : class;
        void Remove(string cacheKey);
    }
}
