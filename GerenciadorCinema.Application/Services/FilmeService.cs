using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Application.Interfaces.Services;

namespace GerenciadorCinema.Application.Services;

public class FilmeService : IFilmeService
{
    private readonly IFilmeQuery _filmeQuery;

    public FilmeService(IFilmeQuery filmeQuery)
    {
        _filmeQuery = filmeQuery;
    }

    public async Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken)
    {
        return await _filmeQuery.GetListAsync(request, cancellationToken);
    }

    public async Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _filmeQuery.GetByIdAsync(id, cancellationToken);
    }
}
