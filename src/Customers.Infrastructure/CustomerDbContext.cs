using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options, IPublishEndpoint publishEndpoint)
    : BaseDbContext(options, publishEndpoint)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
    }
}