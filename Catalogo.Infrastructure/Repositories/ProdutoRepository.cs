using Catalogo.Domain.Entities;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;

namespace Catalogo.Infrastructure.Repositories;

public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context)
    {
    }       
}
