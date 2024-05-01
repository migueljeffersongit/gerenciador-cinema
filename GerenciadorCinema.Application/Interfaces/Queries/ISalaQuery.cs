using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Salas;

namespace GerenciadorCinema.Application.Interfaces.Queries;

public interface ISalaQuery
{
    Task<PaginationResponse<SalaResponseDto>> GetListAsync(GetListaSalaQueryDto request, CancellationToken cancellationToken);
    Task<SalaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
