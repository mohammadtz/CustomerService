namespace Common.DomainBase;

public class AggregateRoot : BaseEntity
{
    private readonly List<IDomainEvent> domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent eventItem) => domainEvents.Add(eventItem);
    
    public void ClearDomainEvents() => domainEvents.Clear();
}