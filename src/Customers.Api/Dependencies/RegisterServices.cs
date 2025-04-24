using Common.DomainBase;
using Customers.Domain.Service;
using Customers.Domain.Services;
using Microsoft.AspNetCore.Diagnostics;

namespace Customers.Api.Dependencies;

public static class RegisterServices
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.Scan(x =>
            x.FromAssemblies(typeof(ICustomerBaseInfoDuplicationChecker).Assembly,
                    typeof(CustomerBaseInfoDuplicationChecker).Assembly)
                .AddClasses(y => y.AssignableTo<IDomainService>()).AsImplementedInterfaces());

        services.Scan(x => x.FromAssemblies(typeof(Program).Assembly)
            .AddClasses(y => y.AssignableTo<IExceptionHandler>()).AsImplementedInterfaces().WithSingletonLifetime());
    }
}