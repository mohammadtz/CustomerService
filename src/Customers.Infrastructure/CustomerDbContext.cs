using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
    }
}