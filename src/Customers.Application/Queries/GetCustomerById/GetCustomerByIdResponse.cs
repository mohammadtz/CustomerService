using Customers.Domain;
using Customers.Domain.Customers;

namespace Customers.Application.Queries.GetCustomerById;

public class GetCustomerByIdResponse(Customer customer)
{
    public Guid Id { get; set; } = customer.Id;
    public string FirstName { get; set; } = customer.BasicInfo.FirstName;
    public string LastName { get; set; } = customer.BasicInfo.LastName;
    public DateOnly DateOfBirth { get; set; } = customer.BasicInfo.DateOfBirth;
    public string? PhoneNumber { get; set; } = customer.PhoneNumber;
    public string? Email { get; set; } = customer.Email;
    public string? BankAccount { get; set; } = customer.BankAccountNumber!;
}