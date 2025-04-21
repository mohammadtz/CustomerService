using Common.DomainBase;

namespace Customers.Domain.Services;

public interface IPhoneNumberValidator : IDomainService
{
    bool Validate(string phoneNumber);
}