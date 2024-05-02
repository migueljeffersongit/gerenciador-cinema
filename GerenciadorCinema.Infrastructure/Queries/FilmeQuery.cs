using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Queries;

public class FilmeQuery : IFilmeQuery
{
    private readonly ApplicationDbContext _context;

    public FilmeQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExisteSala(Guid salaId, CancellationToken cancellationToken)
    {
        return _context.Salas.AnyAsync(x => x.Id == salaId, cancellationToken);
    }

    public async Task<FilmeResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = await _context.Filmes
            .AsNoTracking()
            .Include(x => x.Sala)
            .Select(x => new FilmeResponseDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Duracao = x.Duracao,
                Diretor = x.Diretor,
                SalaId = x.SalaId,

                Sala = new SalaDto
                {
                    NumeroSala = x.Sala.NumeroSala,
                    Descricao = x.Sala.Descricao
                }
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return query;
    }

    public async Task<PaginationResponse<FilmeResponseDto>> GetListAsync(GetListaFilmeQueryDto request, CancellationToken cancellationToken)
    {
        IQueryable<Filme> query = _context.Filmes.AsNoTracking();

        if (!string.IsNullOrEmpty(request.Nome))
            query = query.Where(x => x.Nome.Equals(request.Nome));

        if (!string.IsNullOrEmpty(request.Diretor))
            query = query.Where(x => x.Diretor.Equals(request.Diretor));

        if (request.Duracao.HasValue)
            query = query.Where(x => x.Duracao == request.Duracao);

        if (request.SalaId.HasValue)
            query = query.Where(x => x.SalaId == request.SalaId);

        var count = await query.CountAsync();

        var response = await query            
            .Include(x => x.Sala)
            .Select(x => new FilmeResponseDto
            {
                Id = x.Id,
                Nome = x.Nome,
                Duracao = x.Duracao,
                Diretor = x.Diretor,
                SalaId = x.SalaId,

                Sala = new SalaDto
                {
                    NumeroSala = x.Sala.NumeroSala,
                    Descricao = x.Sala.Descricao
                }
            })
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)            
            .ToListAsync(cancellationToken);

        return new PaginationResponse<FilmeResponseDto>(response, count, request.PageNumber, request.PageSize);
    }
}
