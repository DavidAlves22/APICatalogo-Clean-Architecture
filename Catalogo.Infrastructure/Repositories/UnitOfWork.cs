using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;

namespace Catalogo.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository _produtoRepository;
    private ICategoriaRepository _categoriaRepository;
    public ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepository ?? new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepository ?? new CategoriaRepository(_context);
        }
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
