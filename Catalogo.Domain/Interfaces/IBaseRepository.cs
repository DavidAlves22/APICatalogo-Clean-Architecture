namespace Catalogo.Domain.Interfaces;
public interface IBaseRepository<T>
{
    Task<IEnumerable<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
    T Create(T objeto);
    T Update(T objeto);
    void Remove(int id);
}
