namespace Customers.Domain.Customers.Contracts;

public interface ICustomerQueryDbContext
{
    IQueryable<Customer> Customers { get; } 
}