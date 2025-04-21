using System.Linq.Expressions;
using Common.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public abstract class BaseRepository<TEntity>(DbContext dbContext) where TEntity : BaseEntity
{
    public bool IsExist(Expression<Func<TEntity, bool>> expression)
    {
        return dbContext.Set<TEntity>().Any(expression);
    }
}