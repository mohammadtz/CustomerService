using Customers.Domain;
using Customers.Domain.Customers;
using Customers.Domain.Customers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class CustomerQueryDbContext(DbContextOptions options) : DbContext(options), IQueryDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerQueryDbContext).Assembly);
    }

    public IQueryable<Customer> Customers => Set<Customer>();
}