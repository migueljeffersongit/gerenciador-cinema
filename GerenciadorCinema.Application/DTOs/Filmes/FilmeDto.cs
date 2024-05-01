namespace GerenciadorCinema.Application.DTOs.Filmes;

public class FilmeDto
{
    public Guid? Id { get; set; }
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public TimeSpan? Duracao { get; set; }
    public Guid? SalaId { get; set; }
}
