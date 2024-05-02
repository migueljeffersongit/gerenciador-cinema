using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Application.Interfaces.Services;
using GerenciadorCinema.Application.Services;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Domain.Interfaces.UoW;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using GerenciadorCinema.Infrastructure.Queries;
using GerenciadorCinema.Infrastructure.Repositories;
using GerenciadorCinema.Infrastructure.Repositories.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GerenciadorCinema.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFilmeRepository, FilmeRepository>();
        services.AddScoped<ISalaRepository, SalaRepository>();

        services.AddScoped<IFilmeQuery, FilmeQuery>();
        services.AddScoped<ISalaQuery, SalaQuery>();

        services.AddScoped<IFilmeService, FilmeService>();
        services.AddScoped<ISalaService, SalaService>();
        
        services.AddSwaggerConfigurations();

        return services;
    }

    public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Gerenciador Cinema API",
                Version = "v1",
                Description = "API para gerenciamento de salas de cinemas e exibições de filmes.",
                Contact = new OpenApiContact
                {
                    Name = "Miguel Jefferson",
                    Email = "migueljeffersondev@gmail.com",
                    Url = new Uri("https://www.linkedin.com/in/migueljefferson/")
                }
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Por favor, insira o token JWT com Bearer no campo",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
