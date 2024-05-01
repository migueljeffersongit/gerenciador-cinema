using GerenciadorCinema.Domain.Contracts;

namespace GerenciadorCinema.Domain.Entities;

public class Sala : BaseEntity
{
    public string NumeroSala { get; set; }
    public string Descricao { get; set; }
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();

    public Sala() { }

    public Sala(Guid id) : base(id) {  }

    public Sala(string numeroSala, string descricao)
    {
        ValidarDados(numeroSala, descricao);        
        NumeroSala = numeroSala;
        Descricao = descricao;
    }

    public void AtualizarDados(string numeroSala, string descricao)
    {
        ValidarDados(numeroSala, descricao);
        NumeroSala = numeroSala;
        Descricao = descricao;
    }

    private void ValidarDados(string numeroSala, string descricao)
    {
        if (string.IsNullOrWhiteSpace(numeroSala) || numeroSala.Length > 50)
            throw new ArgumentException(nameof(numeroSala), "Número da sala inválido.");

        if (string.IsNullOrWhiteSpace(descricao) || descricao.Length > 250)
            throw new ArgumentException(nameof(descricao), "Descrição inválida.");
    }
    
}
