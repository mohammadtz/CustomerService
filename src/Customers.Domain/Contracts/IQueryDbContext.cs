namespace Customers.Domain.Contracts;

public interface IQueryDbContext
{
    IQueryable<Customer> Customers { get; } 
}