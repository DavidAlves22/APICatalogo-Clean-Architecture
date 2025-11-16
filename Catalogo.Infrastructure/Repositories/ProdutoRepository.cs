using Catalogo.Application.Repositories;
using Catalogo.Domain.Entities;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Mapper;
using Catalogo.Infrastructure.Models;
namespace Catalogo.Infrastructure.Repositories;

public class ProdutoRepository : BaseRepository<Produto, ProdutoModel>, IProdutoRepository
{
    public ProdutoRepository(ApplicationDbContext context) : base(context, ProdutoMapper.ToModel, ProdutoMapper.ToDomain)
    {
    }
    
    public async Task<Produto> EntradaEstoqueProduto(Produto produto)
    {
        _context.Set<ProdutoModel>().Update(produto.ToModel());
        return produto;
    }
}
