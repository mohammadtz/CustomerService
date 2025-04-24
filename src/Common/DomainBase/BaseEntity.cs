namespace Common.DomainBase;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
}