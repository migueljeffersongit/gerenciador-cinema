using GerenciadorCinema.Application.DTOs.Salas;

namespace GerenciadorCinema.Application.DTOs.Filmes;

public class FilmeResponseDto : FilmeDto
{
    public Guid? Id { get; set; }
    public SalaDto? Sala { get; set; }
}
