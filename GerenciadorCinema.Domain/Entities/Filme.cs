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

    public Filme(string nome, string diretor, TimeSpan duracao, Guid? salaId = null)
    {
        AtualizarDados(nome, diretor, duracao, salaId);
    }

    public void AtualizarDados(string nome, string diretor, TimeSpan duracao, Guid? salaId = null)
    {
        ValidarDados(nome, diretor, duracao, salaId);
        Nome = nome;
        Diretor = diretor;
        Duracao = duracao;
        SalaId = salaId;
    }

    private void ValidarDados(string nome, string diretor, TimeSpan duracao, Guid? salaId)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do filme é obrigatório e não pode ser vazio.");
        if (nome.Length > 100)
            throw new ArgumentException("O nome do filme não pode exceder 100 caracteres.");

        if (string.IsNullOrWhiteSpace(diretor))
            throw new ArgumentException("O nome do diretor é obrigatório e não pode ser vazio.");
        if (diretor.Length > 100)
            throw new ArgumentException("O nome do diretor não pode exceder 100 caracteres.");

        if (duracao <= TimeSpan.Zero)
            throw new ArgumentException("A duração do filme deve ser maior que zero.");
        if (duracao.TotalMinutes > 300)
            throw new ArgumentException("A duração do filme não pode ser maior que 300 minutos.");

        if (salaId == Guid.Empty)
            throw new ArgumentException("Se fornecido, o ID da sala deve ser um GUID válido.");
    }

    public void AlterarSala(Guid? salaId)
    {
        if (salaId != null && salaId == Guid.Empty)
            throw new ArgumentException("Se fornecido, o ID da sala deve ser um GUID válido.");

        SalaId = salaId;
    }

}
