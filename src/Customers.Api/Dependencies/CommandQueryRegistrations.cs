using Common.CommandQueryBase;
using Customers.Application.Commands.CreateCustomer;

namespace Customers.Api.Dependencies;

public static class CommandQueryRegistrations
{
    public static void AddCommandQueryRegistrations(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreateCustomerCommand).Assembly));
    }
}