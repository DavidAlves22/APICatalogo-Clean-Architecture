using Catalogo.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Catalogo.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using Catalogo.Infrastructure.Models;

namespace Catalogo.IntegrationTests.Factories;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public CustomWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove o DbContext registrado originalmente
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Registra o AppDbContext apontando para o MySQL de Testcontainers
            services.ConfigureDataBase(_connectionString);

            // Garante que o banco foi criado (e opcionalmente faz seed)
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            db.Database.Migrate();
            db.Categorias.Add(new CategoriaModel
            {
                Id = 1,
                Nome = "Categoria de Teste",
                ImagemUrl = "categoria.jpg",
                DataCadastro = DateTime.UtcNow
            });

            db.Produtos.Add(new ProdutoModel
            {
                Nome = "Produto de Teste",
                Descricao = "Descrição do Produto de Teste",
                Preco = 99.99m,
                Estoque = 10,
                CategoriaId = 1,
                ImagemUrl = "produto.jpg",
                DataCadastro = DateTime.UtcNow
            });

            db.SaveChanges();
            // Para seed, usar db.Add(...);
            // db.SaveChanges();
        });
    }
}
