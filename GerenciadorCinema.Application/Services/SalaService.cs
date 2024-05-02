using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Exceptions;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Application.Interfaces.Services;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Domain.Interfaces.UoW;

namespace GerenciadorCinema.Application.Services;

public class SalaService : ISalaService
{
    private readonly ISalaQuery _salaQuery;
    private readonly ISalaRepository _salaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SalaService(ISalaQuery salaQuery, ISalaRepository salaRepository, IUnitOfWork unitOfWork)
    {
        _salaQuery = salaQuery;
        _salaRepository = salaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SalaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _salaQuery.GetByIdAsync(id, cancellationToken);
    }

    public async Task<PaginationResponse<SalaResponseDto>> GetListAsync(GetListaSalaQueryDto request, CancellationToken cancellationToken)
    {
        return await _salaQuery.GetListAsync(request, cancellationToken);
    }

    public async Task<SalaResponseDto> AddAsync(AddSalaDto salaDto, CancellationToken cancellationToken)
    {
        var sala = new Sala(salaDto.NumeroSala, salaDto.Descricao);
        await _salaRepository.AddAsync(sala);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new SalaResponseDto
        {
            Id = sala.Id,
            NumeroSala = sala.NumeroSala,
            Descricao = sala.Descricao
        };
    }

    public async Task<SalaResponseDto> UpdateAsync(Guid id, UpdateSalaDto salaDto, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.GetByIdAsync(id, cancellationToken);

        if (sala == null)
            throw new NotFoundException("Sala não encontrada");

        sala.AtualizarDados(salaDto.NumeroSala, salaDto.Descricao);
        await _salaRepository.UpdateAsync(sala);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new SalaResponseDto
        {
            Id = sala.Id,
            NumeroSala = sala.NumeroSala,
            Descricao = sala.Descricao
        };

    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var sala = await _salaRepository.GetByIdAsync(id, cancellationToken);

        if (sala == null)
            throw new NotFoundException("Sala não encontrada");

        await _salaRepository.DeleteAsync(sala);
        await _unitOfWork.CommitAsync(cancellationToken);
    }

}
