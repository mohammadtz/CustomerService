namespace Common.DomainBase;

public interface IDomainEvent
{
    Guid AggregateId { get; }
}