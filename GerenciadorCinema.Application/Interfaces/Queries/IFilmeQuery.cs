using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;

namespace GerenciadorCinema.Application.Interfaces.Queries;

public interface IFilmeQuery
{
    Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken);
    Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExisteSala(Guid salaId, CancellationToken cancellationToken);
}
