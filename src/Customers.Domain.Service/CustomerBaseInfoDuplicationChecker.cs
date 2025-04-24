using Customers.Domain.Customers.Contracts;
using Customers.Domain.Customers.Services;

namespace Customers.Domain.Service;

public class CustomerBaseInfoDuplicationChecker(ICustomerRepository customerRepository)
    : ICustomerBaseInfoDuplicationChecker
{
    public bool IsDuplicate(string firstName, string lastName, DateOnly dateOfBirth)
    {
        return customerRepository.IsExist(x =>
            x.BasicInfo.FirstName == firstName &&
            x.BasicInfo.LastName == lastName &&
            x.BasicInfo.DateOfBirth == dateOfBirth);
    }
}