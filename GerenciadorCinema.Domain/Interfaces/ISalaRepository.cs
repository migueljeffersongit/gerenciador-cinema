using GerenciadorCinema.Domain.Entities;

namespace GerenciadorCinema.Domain.Interfaces;

public interface ISalaRepository
{
    Task AddAsync(Sala sala, CancellationToken cancellationToken = default);
    Task UpdateAsync(Sala sala, CancellationToken cancellationToken = default);
    Task<Sala> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(Sala sala, CancellationToken cancellationToken = default);
}
