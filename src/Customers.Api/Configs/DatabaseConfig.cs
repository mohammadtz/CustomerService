using Customers.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Customers.Api.Configs;

public static class DatabaseConfig
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();

        var dbContext = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();

        dbContext.Database.Migrate();
    }
}