using Common.DomainBase;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Customers.Domain.Customers.DomainEvents;

public class CustomerCreatedEvent : IDomainEvent
{
    public CustomerCreatedEvent(Guid customerId, string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        AggregateId = customerId;
    }

    public CustomerCreatedEvent() { }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AggregateId { get; set; }
}