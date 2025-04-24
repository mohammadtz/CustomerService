namespace Customers.Api.Dependencies;

public static class RegisterCorsConfig
{
    public const string CorsPolicy = "AllowAll";

    public static void AddCorsConfig(this IServiceCollection services)
    {
        const string corsPolicyName = "AllowAll";

        services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyName, policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}