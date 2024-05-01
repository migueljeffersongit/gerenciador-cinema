using GerenciadorCinema.Domain.Entities;

namespace GerenciadorCinema.Domain.Interfaces;

public interface IFilmeRepository
{
    Task AddAsync(Filme filme, CancellationToken cancellationToken = default);
    Task UpdateAsync(Filme filme, CancellationToken cancellationToken = default);
    Task<Filme> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Filme filme, CancellationToken cancellationToken = default);
}
