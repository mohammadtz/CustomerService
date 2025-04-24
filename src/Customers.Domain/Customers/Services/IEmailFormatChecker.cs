using Common.DomainBase;

namespace Customers.Domain.Customers.Services;

public interface IEmailFormatChecker : IDomainService
{
    bool IsValid(string email);
}