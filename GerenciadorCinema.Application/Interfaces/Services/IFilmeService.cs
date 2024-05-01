using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;

namespace GerenciadorCinema.Application.Interfaces.Services;

public interface IFilmeService
{
    Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken);
    Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<FilmeResponseDto> AddAsync(AddFilmeDto filmeDto, CancellationToken cancellationToken);
    Task<FilmeResponseDto> UpdateAsync(Guid id, UpdateFilmeDto filmeDto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
