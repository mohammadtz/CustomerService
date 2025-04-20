namespace Customers.Domain.Customers.Services;

public interface IPhoneNumberValidator
{
    bool Validate(string phoneNumber);
}