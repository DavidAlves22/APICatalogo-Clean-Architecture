using Catalogo.Domain.Entities;
using Catalogo.Infrastructure.Models;

namespace Catalogo.Infrastructure.Mapper
{
    public static class ProdutoMapper
    {
        public static Produto ToDomain(this ProdutoModel produto)
        {
            return new Produto(produto.Id, produto.Nome, produto.Descricao, produto.Preco, produto.ImagemUrl, produto.Estoque, produto.CategoriaId);
        }

        public static ProdutoModel ToModel(this Produto produto)
        {
            return new ProdutoModel
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
