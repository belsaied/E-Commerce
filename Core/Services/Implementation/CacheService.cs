using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementation
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedValueAsync(string key)
            => await _cacheRepository.GetAsync(key);


        public async Task SetCacheValueAsync(string key, object value, TimeSpan duration)
            => await _cacheRepository.SetAsync(key, value, duration); 
    }
}
