using Catalogo.Application.DTOs.Categoria;
using Catalogo.Domain.Entities;

namespace Catalogo.Application.Mappings;

public static class CategoriaMapper
{
    public static Categoria ToDomain(this CategoriaDTO categoria)
    {
        return new Categoria(categoria.Id, categoria.Nome!, categoria.ImagemUrl!);
    }

    public static Categoria CreateToDomain(this CategoriaCreateDTO categoria)
    {
        return new Categoria(0, categoria.Nome!, categoria.ImagemUrl!);
    }

    public static CategoriaDTO ToDTO(this Categoria categoria)
    {
        return new CategoriaDTO
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }
}
