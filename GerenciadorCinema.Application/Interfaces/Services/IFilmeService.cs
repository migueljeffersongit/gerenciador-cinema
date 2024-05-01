using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;

namespace GerenciadorCinema.Application.Interfaces.Services;

public interface IFilmeService
{
    Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken);
    Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
