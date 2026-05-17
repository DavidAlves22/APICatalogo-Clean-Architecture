using Catalogo.Application.Repositories;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;

namespace Catalogo.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository _produtoRepository;
    private ICategoriaRepository _categoriaRepository;
    private ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            if (_produtoRepository == null)
                _produtoRepository = new ProdutoRepository(_context);

            return _produtoRepository;
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            if (_categoriaRepository == null)
                _categoriaRepository = new CategoriaRepository(_context);

            return _categoriaRepository;
        }
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
