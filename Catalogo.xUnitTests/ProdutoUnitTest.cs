using Catalogo.Domain.Entities;

namespace Catalogo.xUnitTests
{
    public class ProdutoUnitTest
    {
        [Fact]
        public void AdicionarEstoque_Deve_SomarQuantidade()
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 0, 1);
            // Act
            produto.AdicionarEstoque(10);
            // Assert
            Assert.Equal(10, produto.Estoque);
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void AdicionarEstoque_NaoPermitir_QuantidadeMenorIgualZero(int quantidade)
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 0, 1);

            // Act
            var exception = Assert.Throws<ArgumentException>(() => produto.AdicionarEstoque(quantidade));

            // Assert
            Assert.Equal("Quantidade deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void RemoverEstoque_Deve_SubtrairQuantidade()
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 20, 1);
            // Act
            produto.RemoverEstoque(5);
            // Assert
            Assert.Equal(15, produto.Estoque);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(0)]
        public void RemoverEstoque_NaoPermitir_QuantidadeMenorIgualZero(int quantidade)
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 20, 1);
            // Act
            var exception = Assert.Throws<ArgumentException>(() => produto.RemoverEstoque(quantidade));
            // Assert
            Assert.Equal("Quantidade deve ser maior que zero.", exception.Message);
        }

        [Fact]
        public void AtualizarCategoria_Deve_AlterarCategoriaId_QuandoValido()
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 20, 1);

            // Act
            produto.AtualizarCategoria(2);

            // Assert
            Assert.Equal(2, produto.CategoriaId);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void AtualizarCategoria_NaoDeve_AlterarCategoriaId_QuandoInvalido(int categoriaId) 
        {
            // Arrange
            var produto = new Produto(1, "Produto Teste", "Descricao Teste", 10.0m, "imagem.jpg", 20, 1);

            // Act
            var exception = Assert.Throws<ArgumentException>(() => produto.AtualizarCategoria(categoriaId));

            // Assert
            Assert.Equal("CategoriaId deve ser maior que zero.", exception.Message);
        }
    }
}
