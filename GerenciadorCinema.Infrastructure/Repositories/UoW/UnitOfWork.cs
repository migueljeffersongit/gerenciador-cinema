using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Domain.Interfaces.UoW;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Repositories.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _isDisposed;
    private ISalaRepository? _salaRepository;
    private IFilmeRepository? _filmeRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ISalaRepository SalaRepository => _salaRepository ??= new SalaRepository(_context);

    public IFilmeRepository FilmeRepository => _filmeRepository ??= new FilmeRepository(_context);

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public bool TryCommit()
    {
        return _context.SaveChanges() > 0;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.ReloadAsync(cancellationToken);
                    break;
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            _context.Dispose();
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            await DisposeAsyncCore();
        }

        Dispose(false);
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await _context.DisposeAsync();
    }

}
