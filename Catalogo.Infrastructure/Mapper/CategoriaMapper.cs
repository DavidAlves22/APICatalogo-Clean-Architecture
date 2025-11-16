using Catalogo.Domain.Entities;
using Catalogo.Infrastructure.Models;

namespace Catalogo.Infrastructure.Mapper;

public static class CategoriaMapper
{
    public static Categoria ToDomain(this CategoriaModel categoria)
    {
        return new Categoria(categoria.Id, categoria.Nome!, categoria.ImagemUrl!);
    }

    public static CategoriaModel ToModel(this Categoria categoria)
    {
        return new CategoriaModel
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl,
            DataCadastro = categoria.DataCadastro
        };
    }
}
