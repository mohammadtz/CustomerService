using Common.InfrastructureBase;

namespace Customers.Infrastructure;

public class UnitOfWork(CustomerDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}