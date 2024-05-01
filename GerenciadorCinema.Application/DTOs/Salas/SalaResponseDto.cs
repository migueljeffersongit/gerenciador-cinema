using GerenciadorCinema.Application.DTOs.Filmes;

namespace GerenciadorCinema.Application.DTOs.Salas;

public class SalaResponseDto : SalaDto
{
    public Guid? Id { get; set; }
    public List<FilmeResponseDto>? Filmes { get; set; }
}
