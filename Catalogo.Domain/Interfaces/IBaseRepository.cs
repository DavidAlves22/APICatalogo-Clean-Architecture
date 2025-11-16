namespace Catalogo.Domain.Interfaces;
public interface IBaseRepository<TEntity, TModel>
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetByIdAsync(int id);
    TEntity Create(TEntity objeto);
    TEntity Update(TEntity objeto);
    void Remove(int id);
}
