using GerenciadorCinema.Application.Common;

namespace GerenciadorCinema.Application.DTOs.Salas;

public class GetListaSalaQueryDto : PaginationFilter
{
    public string? NumeroSala { get; set; }
    public string? Descricao { get; set; }
}
