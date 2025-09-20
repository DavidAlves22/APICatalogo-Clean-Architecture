using Catalogo.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Catalogo.Application.Services;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public string GetCategoriasCacheKey(string cacheKey, int id)
    {
        return $"{cacheKey}_{id}";
    }

    public void SetCache<T>(string key, T item)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30))
            .SetSlidingExpiration(TimeSpan.FromSeconds(10))
            .SetPriority(CacheItemPriority.High);

        _cache.Set(key, item, cacheOptions);
    }

    public void LimparCache(string cacheKey)
    {
        _cache.Remove(cacheKey);
    }

    public void RemoverCache(string cacheKey, int idRemover)
    {
        _cache.Remove(cacheKey);
        _cache.Remove(GetCategoriasCacheKey(cacheKey, idRemover));
    }

    public bool ExisteCache<T>(string chave, out T valor)
    {
        if (_cache.TryGetValue(chave, out var result) && result is T castValue)
        {
            valor = castValue;
            return true;
        }

        valor = default!;
        return false;
    }
}
