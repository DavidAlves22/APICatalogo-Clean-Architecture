using Catalogo.Application.Repositories;
using Catalogo.Domain.Entities;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Mapper;
using Catalogo.Infrastructure.Models;

namespace Catalogo.Infrastructure.Repositories;

public class CategoriaRepository : BaseRepository<Categoria, CategoriaModel>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context, CategoriaMapper.ToModel, CategoriaMapper.ToDomain)
    {
    }
}
