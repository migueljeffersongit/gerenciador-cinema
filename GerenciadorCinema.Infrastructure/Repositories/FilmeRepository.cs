using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Repositories;

public class FilmeRepository : IFilmeRepository
{
    private readonly ApplicationDbContext _context;

    public FilmeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Filme filme, CancellationToken cancellationToken = default)
    {
        await _context.Filmes.AddAsync(filme, cancellationToken);
    }

    public async Task DeleteAsync(Filme filme, CancellationToken cancellationToken = default)
    {
        _context.Filmes.Remove(filme);
    }

    public async Task<Filme> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Filmes.Include(x => x.Sala).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Filme filme, CancellationToken cancellationToken = default)
    {
        _context.Filmes.Update(filme);
    }
}
