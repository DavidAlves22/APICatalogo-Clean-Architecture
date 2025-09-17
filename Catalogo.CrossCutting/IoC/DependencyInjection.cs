using Catalogo.Application.Mappings;
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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        var mySQLConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(optionsAction =>
        {
            optionsAction.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection));                                                                                                 
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();

        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        services.AddScoped<ITokenService, TokenService>();

        MapsterConfig.ConfigurarMapeamento();
        services.AddMapster();

        return services;
    }
}
