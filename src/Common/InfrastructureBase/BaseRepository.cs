using System.Linq.Expressions;
using Common.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Common.InfrastructureBase;

public abstract class BaseRepository<TEntity>(DbContext dbContext) where TEntity : BaseEntity
{
    public bool IsExist(Expression<Func<TEntity, bool>> expression)
    {
        return dbContext.Set<TEntity>().Any(expression);
    }

    public void Create(TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
    }
}