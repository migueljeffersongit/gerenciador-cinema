using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCinema.Infrastructure.Queries;

public class SalaQuery : ISalaQuery
{
    private readonly ApplicationDbContext _context;

    public SalaQuery(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SalaResponseDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = await _context.Salas
            .AsNoTracking()
            .Include(x => x.Filmes)
            .Select(x => new SalaResponseDto
            {
                Id = x.Id,
                NumeroSala = x.NumeroSala,
                Descricao = x.Descricao,

                Filmes = new List<FilmeDto>(x.Filmes.Select(f => new FilmeDto
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    Duracao = f.Duracao,
                    Diretor = f.Diretor,
                    SalaId = f.SalaId,

                })).ToList()

            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


        return query;

    }

    public async Task<PaginationResponse<SalaResponseDto>> GetListAsync(GetListaSalaQueryDto request, CancellationToken cancellationToken)
    {
        IQueryable<Sala> query = _context.Salas.AsNoTracking();

        if (!string.IsNullOrEmpty(request.NumeroSala))
            query = query.Where(x => x.NumeroSala.Equals(request.NumeroSala));

        if (!string.IsNullOrEmpty(request.Descricao))
            query = query.Where(x => x.Descricao.Equals(request.Descricao));

        var count = await query.CountAsync();

        var salas = await query
            .Include(x => x.Filmes)           
            .Select(x => new SalaResponseDto
            {
                Id = x.Id,
                NumeroSala = x.NumeroSala,
                Descricao = x.Descricao,

                Filmes = new List<FilmeDto>(x.Filmes.Select(f => new FilmeDto
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    Duracao = f.Duracao,
                    Diretor = f.Diretor,
                    SalaId = f.SalaId,

                })).ToList()

            })
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PaginationResponse<SalaResponseDto>(salas, count, request.PageNumber, request.PageSize);
    }
}
