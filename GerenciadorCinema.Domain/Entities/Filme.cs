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

    public Filme(string nome, string diretor, TimeSpan duracao, Guid? salaId)
    {
        ValidarDados(nome, diretor, duracao, salaId);
        Nome = nome;
        Diretor = diretor;
        Duracao = duracao;
        SalaId = salaId;
    }

    public void AtualizarDados(string nome, string diretor, TimeSpan duracao, Guid? salaId)
    {
        ValidarDados(nome, diretor, duracao, salaId);
        Nome = nome;
        Diretor = diretor;
        Duracao = duracao;
        SalaId = salaId;
    }

    private void ValidarDados(string nome, string diretor, TimeSpan duracao, Guid? salaId)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Length > 100)
            throw new ArgumentException(nameof(nome), "O nome do filme é obrigatório.");
        if (string.IsNullOrWhiteSpace(diretor) || diretor.Length > 100)
            throw new ArgumentException(nameof(diretor), "O diretor do filme é obrigatório.");
        if (duracao == TimeSpan.Zero)
            throw new ArgumentNullException(nameof(duracao), "A duração do filme é obrigatória.");
        if(duracao.TotalMinutes < 0)
            throw new ArgumentException(nameof(duracao), "A duração do filme não pode ser negativa.");
        if(duracao.TotalMinutes > 300)
            throw new ArgumentException(nameof(duracao), "A duração do filme não pode ser maior que 300 minutos.");
        if(salaId == Guid.Empty)
            throw new ArgumentNullException(nameof(salaId), "Id da sala inválido.");
    }

    public void AlterarSala(Guid salaId)
    {
        if (salaId == default || salaId == Guid.Empty)
            throw new ArgumentNullException(nameof(salaId), "Id da sala inválido");

        SalaId = salaId;
    }

}
