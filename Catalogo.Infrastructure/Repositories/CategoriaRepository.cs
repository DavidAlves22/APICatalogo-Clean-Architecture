using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;

namespace Catalogo.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
    }
}
