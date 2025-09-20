using Catalogo.Application.DTOs;
using Catalogo.Domain.Entities;
using Mapster;

namespace Catalogo.Application.Mappings;

public class MapsterConfig
{
    public static void ConfigurarMapeamento()
    {
        TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.FromCamelCase);

        TypeAdapterConfig<Produto, ProdutoDTO>.NewConfig().TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Nome, src => src.Nome)
            .Map(dest => dest.Descricao, src => src.Descricao)
            .Map(dest => dest.Preco, src => src.Preco)
            .Map(dest => dest.ImagemUrl, src => src.ImagemUrl)
            .Map(dest => dest.Estoque, src => src.Estoque)
            .Map(dest => dest.DataCadastro, src => src.DataCadastro)
            .Map(dest => dest.CategoriaId, src => src.CategoriaId);

        TypeAdapterConfig<Categoria, CategoriaDTO>.NewConfig().TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Nome, src => src.Nome)
            .Map(dest => dest.ImagemUrl, src => src.ImagemUrl);
    }
}
