﻿using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.DTOs.Salas;

namespace GerenciadorCinema.Application.Interfaces.Services;

public interface ISalaService
{
    Task<PaginationResponse<SalaResponseDto>> GetListAsync(GetListaSalaQueryDto request, CancellationToken cancellationToken);
    Task<SalaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
