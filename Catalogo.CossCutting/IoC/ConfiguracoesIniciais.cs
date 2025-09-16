using Catalogo.Infrastructure.Context;
using Catalogo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Catalogo.CrossCutting.IoC;

public static class ConfiguracoesIniciais
{
    public static IServiceCollection AddConfiguracoesIniciais(this IServiceCollection services, IConfiguration configuration)
    {
        AddSwagger(services);
        AddAuthorization(services);
        AddAuthentication(services, configuration);
        return services;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer");
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        var secretKey = configuration["JWT:SecretKey"] ?? throw new ArgumentNullException("Invalid Secret Key!!");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false; // Desabilita a exigência de HTTPS para desenvolvimento (Em produção, deve ser true)
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero, // Reduz o tempo de tolerância para a expiração do token
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

            options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin").RequireClaim("id", "cti"));

            options.AddPolicy("UserOnly", policy => policy.RequireRole("Usuario"));

            options.AddPolicy("ExclusiveOnly", policy => policy.RequireAssertion(context => context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "cti") || context.User.IsInRole("SuperAdmin")));

        });
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "APICatalogo", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = @"Bearer JWT"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}
