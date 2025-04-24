using Customers.Api.Options;
using Customers.Domain.Customers.DomainEvents;
using Customers.Infrastructure.Messaging.Consumers;
using MassTransit;

namespace Customers.Api.Dependencies;

public static class RegisterMassTransit
{
    public static void AddMassTransitConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection("RabbitMqOptions").Get<RabbitMqOptions>()!;

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CustomerCreatedConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Url, "/", h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ReceiveEndpoint("customer-created-event-queue", e =>
                    e.ConfigureConsumer<CustomerCreatedConsumer>(context));
            });
        });
    }
}