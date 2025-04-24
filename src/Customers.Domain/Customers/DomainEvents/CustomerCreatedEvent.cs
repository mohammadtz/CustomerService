using Common.DomainBase;

namespace Customers.Domain.Customers.DomainEvents;

public class CustomerCreatedEvent : IDomainEvent
{
    public CustomerCreatedEvent(Guid customerId, string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        AggregateId = customerId;
    }

    public CustomerCreatedEvent()
    {
        
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid AggregateId { get; set; }
}