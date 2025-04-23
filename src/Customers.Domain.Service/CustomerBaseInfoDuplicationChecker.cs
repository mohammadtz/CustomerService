using Customers.Domain.Contracts;
using Customers.Domain.Services;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Service;

public class CustomerBaseInfoDuplicationChecker(ICustomerRepository customerRepository)
    : ICustomerBaseInfoDuplicationChecker
{
    public bool IsDuplicate(string firstName, string lastName, DateOnly dateOfBirth)
    {
        var basicInfo = new CustomerBasicInfo(firstName, lastName, dateOfBirth);
        return customerRepository.IsExist(x => x.BasicInfo == basicInfo);
    }
}