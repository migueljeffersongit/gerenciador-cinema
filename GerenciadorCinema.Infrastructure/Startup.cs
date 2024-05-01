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

        return services;
    }
}
