using Catalogo.Domain.Entities;

namespace Catalogo.Application.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAsync();
    Task<Produto> GetByIdAsync(int id);
    Produto Create(Produto objeto);
    Produto Update(Produto objeto);
    void Remove(int id);
}
