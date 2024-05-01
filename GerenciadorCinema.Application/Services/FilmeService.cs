using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Application.Interfaces.Services;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Domain.Interfaces.UoW;

namespace GerenciadorCinema.Application.Services;

public class FilmeService : IFilmeService
{
    private readonly IFilmeQuery _filmeQuery;
    private readonly IFilmeRepository _filmeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FilmeService(IFilmeQuery filmeQuery, IFilmeRepository filmeRepository, IUnitOfWork unitOfWork)
    {
        _filmeQuery = filmeQuery;
        _filmeRepository = filmeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken)
    {
        return await _filmeQuery.GetListAsync(request, cancellationToken);
    }

    public async Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _filmeQuery.GetByIdAsync(id, cancellationToken);
    }

    public async Task<FilmeResponseDto> AddAsync(AddFilmeDto filmeDto, CancellationToken cancellationToken)
    {
        var filme = new Filme(filmeDto.Nome, filmeDto.Diretor, (TimeSpan)filmeDto.Duracao, filmeDto.SalaId);
        await _filmeRepository.AddAsync(filme);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new FilmeResponseDto
        {
            Id = filme.Id,
            Nome = filme.Nome,
            Diretor = filme.Diretor,
            Duracao = filme.Duracao,
            SalaId = filme.SalaId,            
        };
    }

    public async Task<FilmeResponseDto> UpdateAsync(Guid id, UpdateFilmeDto filmeDto, CancellationToken cancellationToken)
    {
        var filme = await _filmeRepository.GetByIdAsync(id, cancellationToken);

        if (filme == null)
            throw new Exception("Filme não encontrado");

        filme.AtualizarDados(filmeDto.Nome, filmeDto.Diretor, (TimeSpan)filmeDto.Duracao, filmeDto.SalaId);
        await _filmeRepository.UpdateAsync(filme);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new FilmeResponseDto
        {
            Id = filme.Id,
            Nome = filme.Nome,
            Diretor = filme.Diretor,
            Duracao = filme.Duracao,
            SalaId = filme.SalaId            
        };

    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var filme = await _filmeRepository.GetByIdAsync(id, cancellationToken);

        if (filme == null)
            throw new Exception("Filme não encontrado");

        await _filmeRepository.DeleteAsync(filme);
        await _unitOfWork.CommitAsync(cancellationToken);

    }
}
