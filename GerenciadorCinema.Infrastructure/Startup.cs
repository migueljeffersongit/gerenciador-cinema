using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Domain.Interfaces.UoW;
using GerenciadorCinema.Infrastructure.Database.Contexts;
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

        return services;
    }
}
