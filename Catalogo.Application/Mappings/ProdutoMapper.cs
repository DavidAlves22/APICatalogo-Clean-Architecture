using Catalogo.Application.DTOs;
using Catalogo.Domain.Entities;

namespace Catalogo.Application.Mappings
{
    public static class ProdutoMapper
    {
        public static Produto ToDomain(this ProdutoDTO produto)
        {
            return new Produto(produto.Id, produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId);
        }

        public static ProdutoDTO ToDTO(this Produto produto)
        {
            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                Estoque = produto.Estoque,
                DataCadastro = produto.DataCadastro,
                CategoriaId = produto.CategoriaId
            };
        }
    }
}
