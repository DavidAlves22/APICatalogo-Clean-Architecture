namespace Catalogo.Application.Services.Interfaces;

public interface ICacheService
{
    string GetCategoriasCacheKey(string cacheKey, int id);
    void SetCache<T>(string key, T item);
    void LimparCache(string listaObjetoCache);
    bool ExisteCache<T>(string chave, out T valor);
    void RemoverCache(string cacheKey, int idRemover);
}
