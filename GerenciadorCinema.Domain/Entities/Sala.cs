using GerenciadorCinema.Domain.Contracts;

namespace GerenciadorCinema.Domain.Entities;

public class Sala : BaseEntity
{
    public string NumeroSala { get; set; }
    public string Descricao { get; set; }
    public virtual ICollection<Filme> Filmes { get; set; } = new List<Filme>();

    public Sala() { }

    public Sala(Guid id) : base(id) { }

    public void AtualizarDados(string numeroSala, string descricao)
    {
        NumeroSala = numeroSala;
        Descricao = descricao;
    }

    public bool AdicionarFilme(Filme filme)
    {
        if (filme == null)
            throw new ArgumentException("Filme inválido.");

        if (!Filmes.Any(f => f.Nome.Equals(filme.Nome, StringComparison.OrdinalIgnoreCase) || f.Id == filme.Id))
        {
            Filmes.Add(filme);
            return true;
        }
        return false;
    }


    public bool RemoverFilme(Filme filme)
    {
        if (filme == null)
            throw new ArgumentNullException(nameof(filme), "O filme fornecido é nulo.");

        bool removido = Filmes.Remove(filme);
        return removido;
    }
}
