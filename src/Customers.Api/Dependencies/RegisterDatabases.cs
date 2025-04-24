using Common.DomainBase;
using Common.InfrastructureBase;
using Customers.Domain.Contracts;
using Customers.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Dependencies;

public static class RegisterDatabases
{
    public static void AddDatabasesConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>(x =>
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDbContext<CustomerQueryDbContext>(x =>
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        
        services.AddScoped<IQueryDbContext, CustomerQueryDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.Scan(x =>
            x.FromAssemblies(typeof(CustomerRepository).Assembly, typeof(ICustomerRepository).Assembly)
                .AddClasses(y => y.AssignableTo<IRepositoryBase>()).AsImplementedInterfaces());
    }
}