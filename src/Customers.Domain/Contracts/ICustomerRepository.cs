using System.Linq.Expressions;
using Common.DomainBase;

namespace Customers.Domain.Contracts;

public interface ICustomerRepository : IRepositoryBase
{
    bool IsExist(Expression<Func<Customer, bool>> expression);
}