namespace Common.DomainBase;

public abstract class BaseEntity
{
    public virtual Guid Id { get; private set; } = Guid.CreateVersion7();
}