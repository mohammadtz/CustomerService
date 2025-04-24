using System.Linq.Expressions;
using Common.DomainBase;

namespace Customers.Domain.Customers.Contracts;

public interface ICustomerRepository : IRepositoryBase
{
    bool IsExist(Expression<Func<Customer, bool>> expression);
    void Create(Customer customer);
}