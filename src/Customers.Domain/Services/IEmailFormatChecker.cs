using Common.DomainBase;

namespace Customers.Domain.Services;

public interface IEmailFormatChecker : IDomainService
{
    bool IsValid(string email);
}