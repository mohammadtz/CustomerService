using Common.DomainBase;

namespace Customers.Domain.Customers.Services;

public interface ICustomerBaseInfoDuplicationChecker : IDomainService
{
    bool IsDuplicate(string firstName, string lastName, DateOnly dateOfBirth);
}