using Common.DomainBase;

namespace Customers.Domain.Services;

public interface IEmailDuplicationChecker : IDomainService
{
    bool IsDuplicate(string email, params Guid[] excludedIds);
}