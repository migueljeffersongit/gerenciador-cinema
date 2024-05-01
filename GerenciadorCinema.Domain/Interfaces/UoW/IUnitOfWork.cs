namespace GerenciadorCinema.Domain.Interfaces.UoW;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IFilmeRepository FilmeRepository { get; }
    ISalaRepository SalaRepository { get; }

    Task CommitAsync(CancellationToken cancellationToken = default);
    bool TryCommit();
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
