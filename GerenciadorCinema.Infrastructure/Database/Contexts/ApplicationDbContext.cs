using GerenciadorCinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Database.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Sala> Salas { get; set; }
    public DbSet<Filme> Filmes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
