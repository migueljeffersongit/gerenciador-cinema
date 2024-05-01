using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Application.Interfaces.Services;

namespace GerenciadorCinema.Application.Services;

public class SalaService : ISalaService
{
    private readonly ISalaQuery _salaQuery;

    public SalaService(ISalaQuery salaQuery)
    {
        _salaQuery = salaQuery;
    }

    public async Task<SalaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _salaQuery.GetByIdAsync(id, cancellationToken);
    }

    public async Task<PaginationResponse<SalaResponseDto>> GetListAsync(GetListaSalaQueryDto request, CancellationToken cancellationToken)
    {
        return await _salaQuery.GetListAsync(request, cancellationToken);
    }
}
