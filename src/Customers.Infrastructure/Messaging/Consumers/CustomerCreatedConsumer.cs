using Customers.Domain.Customers.DomainEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.Messaging.Consumers;

public class CustomerCreatedConsumer(ILogger<CustomerCreatedConsumer> logger) : IConsumer<CustomerCreatedEvent>
{
    public async Task Consume(ConsumeContext<CustomerCreatedEvent> context)
    {
        logger.LogInformation("Customer Created successfully");
        await Task.CompletedTask;
    }
}