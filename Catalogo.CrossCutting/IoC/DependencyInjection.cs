using Catalogo.Application.Mappings;
using Catalogo.Application.Repositories;
using Catalogo.Application.Services;
using Catalogo.Application.Services.Interfaces;
using Catalogo.Domain.Interfaces;
using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalogo.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDataBase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(optionsAction =>
        {
            optionsAction.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));                                                                                                 
        });
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        var mySQLConnection = configuration.GetConnectionString("DefaultConnection");

        ConfigureDataBase(services, mySQLConnection);

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICacheService, CacheService>();

        MapsterConfig.ConfigurarMapeamento();
        services.AddMapster();

        return services;
    }
}
