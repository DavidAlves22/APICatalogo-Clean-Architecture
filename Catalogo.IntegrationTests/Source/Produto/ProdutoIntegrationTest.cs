using Catalogo.Application.Services.Interfaces;
using Catalogo.Infrastructure.Models;
using Catalogo.IntegrationTests.Factories;
using Catalogo.IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.IntegrationTests.Source.Produto;

public class ProdutoIntegrationTest : IClassFixture<MySqlFixture>
{
    private readonly MySqlFixture _fixture;
    private readonly CustomWebApplicationFactory _factory;

    public ProdutoIntegrationTest(MySqlFixture fixture)
    {
        _fixture = fixture;
        _factory = new CustomWebApplicationFactory(_fixture.ConnectionString);
    }

    [Fact]
    public async Task Deve_inserir_produto_no_banco()
    {
        using var scope = _factory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var service = serviceProvider.GetRequiredService<IProdutoService>();
        
        //Act
        var resultado = await service.GetProdutos();

        //Assert
        Assert.NotNull(resultado);
    }
}
