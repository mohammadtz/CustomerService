using Common.DomainBase;

namespace Customers.Domain.Customers.Services;

public interface IEmailDuplicationChecker : IDomainService
{
    bool IsDuplicate(string email, params Guid[] excludedIds);
}