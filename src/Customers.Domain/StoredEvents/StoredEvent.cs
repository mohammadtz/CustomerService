namespace Customers.Domain.StoredEvents;

public class StoredEvent
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid AggregateId { get; set; }
    public required string AggregateType { get; set; }
    public required string EventType { get; set; }
    public required string EventData { get; set; }
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}