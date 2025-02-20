namespace TechLibrary.Api.Domain.Entities;

public abstract class Entity : IEquatable<Guid>
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id { get; set; }

    public bool Equals(Guid id)
        => Id == id;
}
