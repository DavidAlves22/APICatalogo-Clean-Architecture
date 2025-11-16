using Catalogo.Domain.Entities;

namespace Catalogo.Application.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetAsync();
    Task<Categoria> GetByIdAsync(int id);
    Categoria Create(Categoria objeto);
    Categoria Update(Categoria objeto);
    void Remove(int id);
}
