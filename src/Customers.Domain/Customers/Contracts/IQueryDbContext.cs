namespace Customers.Domain.Customers.Contracts;

public interface IQueryDbContext
{
    IQueryable<Customer> Customers { get; } 
}