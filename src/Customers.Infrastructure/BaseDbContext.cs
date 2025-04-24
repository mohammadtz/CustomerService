using System.Text.Json;
using Common.DomainBase;
using Customers.Domain.StoredEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public abstract class BaseDbContext(DbContextOptions options, IPublishEndpoint publishEndpoint) : DbContext(options)
{
    public DbSet<StoredEvent> Events { get; set; }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        var storedEvents = domainEvents.Select(e => new StoredEvent
        {
            AggregateId = e.AggregateId,
            AggregateType = e.GetType().Name.Replace("Event", ""),
            EventType = e.GetType().Name,
            EventData = JsonSerializer.Serialize((object)e),
            OccurredOn = DateTime.UtcNow
        }).ToList();

        Events.AddRange(storedEvents);

        foreach (var entry in ChangeTracker.Entries<AggregateRoot>())
        {
            entry.Entity.ClearDomainEvents();
        }

        int result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            Type messageType = domainEvent.GetType();
            object @event = (object)domainEvent;

            await publishEndpoint.Publish(@event, messageType, cancellationToken);
        }

        return result;
    }
}