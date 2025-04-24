using Common.DomainBase;

namespace Customers.Domain.Customers.Services;

public interface IPhoneNumberValidator : IDomainService
{
    bool Validate(string phoneNumber);
}