using Microsoft.Extensions.Caching.Distributed;

namespace BookIt.Infrastructure.Caching;

public static class CacheOptions
{
    public static DistributedCacheEntryOptions defaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) => 
        expiration is not null ? 
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration } : 
            defaultExpiration;
}