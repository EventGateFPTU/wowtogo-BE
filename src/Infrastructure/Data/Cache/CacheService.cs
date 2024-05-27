using Domain.Interfaces.Data.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data.Cache;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public T? GetData<T>(string key)
    {
        if (string.IsNullOrEmpty(key)) return default;
        return _memoryCache.Get<T>(key);
    }

    public void RemoveData(string key)
    {
        if (string.IsNullOrEmpty(key)) return;
        _memoryCache.Remove(key);
    }

    public void SetData<T>(string key, T value, TimeSpan? expirationTime)
    {
        if (string.IsNullOrEmpty(key)) return;
        TimeSpan expiredTime = expirationTime ?? TimeSpan.FromMinutes(5);
        _memoryCache.Set(key, value, expiredTime);
    }
}