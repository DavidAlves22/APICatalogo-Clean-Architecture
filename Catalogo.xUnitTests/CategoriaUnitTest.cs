using Catalogo.Domain.Entities;

namespace Catalogo.xUnitTests;
public class CategoriaUnitTest
{
    [Fact]
    public void CriarCategoria_DeveCriarComDadosValidos()
    {
        var categoria = new Categoria(1, "Doces", "doces.jpg");

        Assert.Equal("Doces", categoria.Nome);
        Assert.Equal("doces.jpg", categoria.ImagemUrl);
        Assert.NotEqual(default, categoria.DataCadastro);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void CriarCategoria_DeveLancarErro_QuandoNomeInvalido(string nome)
    {
        Assert.Throws<ArgumentException>(() => new Categoria(1, nome, "imagem.jpg"));
    }

    [Fact]
    public void AdicionarProduto_DeveAdicionar_ProdutoNaLista_QuandoProdutoValido()
    {
        var categoria = new Categoria(1, "Bebidas", "bebidas.jpg");
        var produto = new Produto(1, "Refrigerante", "Bebida refrescante", 5.00m, "refrigerante.jpg", 50, categoria.Id);

        categoria.AdicionarProduto(produto);

        Assert.Contains(produto, categoria.Produtos);
    }

    [Fact]
    public void AdicionarProduto_NaoDeveAdicionar_ProdutoNaLista_QuandoProdutoInvalido()
    {
        var categoria = new Categoria(1, "Bebidas", "bebidas.jpg");
        // O método AdicionarProduto permite adicionar um produto nulo
        
        var exception = Assert.Throws<ArgumentException>(() => categoria.AdicionarProduto(null!));

        Assert.Equal("Produto não pode ser nulo.", exception.Message);
    }
}
