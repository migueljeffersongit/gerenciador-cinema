using GerenciadorCinema.Domain.Contracts;

namespace GerenciadorCinema.Domain.Entities;

public class Sala : BaseEntity
{
    public string NumeroSala { get; set; }
    public string Descricao { get; set; }
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();

    public Sala() { }

    public Sala(Guid id) : base(id) { }

    public Sala(string numeroSala, string descricao)
    {
        AtualizarDados(numeroSala, descricao);
    }

    public void AtualizarDados(string numeroSala, string descricao)
    {
        ValidarDados(numeroSala, descricao);
        NumeroSala = numeroSala;
        Descricao = descricao;
    }

    private void ValidarDados(string numeroSala, string descricao)
    {
        if (string.IsNullOrWhiteSpace(numeroSala))
            throw new ArgumentException("Número da sala é obrigatório.", nameof(numeroSala));

        if (numeroSala.Length > 50)
            throw new ArgumentException("Número da sala não pode exceder 50 caracteres.", nameof(numeroSala));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        if (descricao.Length > 250)
            throw new ArgumentException("Descrição não pode exceder 250 caracteres.", nameof(descricao));
    }

}
