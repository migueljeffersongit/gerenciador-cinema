using GerenciadorCinema.Application.Common;

namespace GerenciadorCinema.Application.DTOs.Filmes;

public class GetListaFilmeQueryDto : PaginationFilter
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public TimeSpan? Duracao { get; set; }
    public Guid? SalaId { get; set; }
}
