using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Repositories;

public class SalaRepository : ISalaRepository
{
    private readonly ApplicationDbContext _context;

    public SalaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sala sala, CancellationToken cancellationToken = default)
    {
        await _context.Salas.AddAsync(sala, cancellationToken);
    }

    public async Task DeleteAsync(Sala sala, CancellationToken cancellationToken = default)
    {
        _context.Salas.Remove(sala);        
    }

    public async Task<Sala> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Salas.Include(x => x.Filmes).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Sala sala, CancellationToken cancellationToken = default)
    {
        _context.Salas.Update(sala);
    }
}
