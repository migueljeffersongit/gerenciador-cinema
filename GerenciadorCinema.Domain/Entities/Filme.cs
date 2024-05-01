using GerenciadorCinema.Domain.Contracts;

namespace GerenciadorCinema.Domain.Entities;

public class Filme : BaseEntity
{
    public string Nome { get; set; }
    public string Diretor { get; set; }
    public TimeSpan Duracao { get; set; }
    public Guid? SalaId { get; set; }
    public virtual Sala? Sala { get; set; }

    public Filme() { }

    public Filme(Guid id) : base(id) { }

    public void AtualizarDados(string nome, string diretor, TimeSpan duracao)
    {
        Nome = nome;
        Diretor = diretor;
        Duracao = duracao;
    }

    public void AlterarSala(Sala sala)
    {
        if (sala == null)
            throw new ArgumentNullException(nameof(sala), "A sala fornecida é nula.");
        Sala = sala;
    }

}
