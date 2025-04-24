using Common.CommandQueryBase;

namespace Customers.Application.Commands.CreateCustomer;

public class CreateCustomerCommand : ICommand
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
}