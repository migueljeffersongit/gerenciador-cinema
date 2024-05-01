namespace GerenciadorCinema.Domain.Contracts;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = default!;
    protected BaseEntity() : this(Guid.NewGuid()) { }
    protected BaseEntity(Guid id)
    {
        Id = id;
    }
}
