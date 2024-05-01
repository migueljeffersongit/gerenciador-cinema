using GerenciadorCinema.Application.DTOs.Salas;

namespace GerenciadorCinema.Application.DTOs.Filmes;

public class FilmeResponseDto : FilmeDto
{   
    public SalaDto? Sala { get; set; }
}
