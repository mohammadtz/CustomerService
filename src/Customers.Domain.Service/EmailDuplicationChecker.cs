using Customers.Domain.Customers.Contracts;
using Customers.Domain.Customers.Services;

namespace Customers.Domain.Service;

public class EmailDuplicationChecker(ICustomerRepository customerRepository) : IEmailDuplicationChecker
{
    public bool IsDuplicate(string email, params Guid[] excludedIds)
    {
        return customerRepository.IsExist(x => x.Email == email && !excludedIds.Contains(x.Id));
    }
}