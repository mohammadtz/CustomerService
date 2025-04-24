using Common.CommandQueryBase;
using Common.DomainBase;
using Common.InfrastructureBase;
using Customers.Application.Commands.CreateCustomer;
using Customers.Application.Queries.GetCustomerById;
using Customers.Domain.Contracts;
using Customers.Domain.Service;
using Customers.Domain.Services;
using Customers.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CustomerQueryDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddScoped<IQueryDbContext, CustomerQueryDbContext>();

const string corsPolicyName = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IDispatcher, Dispatcher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Scan(x =>
    x.FromAssemblies(typeof(CustomerRepository).Assembly, typeof(ICustomerRepository).Assembly)
        .AddClasses(y => y.AssignableTo<IRepositoryBase>()).AsImplementedInterfaces());

builder.Services.Scan(x =>
    x.FromAssemblies(typeof(ICustomerBaseInfoDuplicationChecker).Assembly,
            typeof(CustomerBaseInfoDuplicationChecker).Assembly)
        .AddClasses(y => y.AssignableTo<IDomainService>()).AsImplementedInterfaces());

builder.Services.Scan(x => x.FromAssemblies(typeof(Program).Assembly)
    .AddClasses(y => y.AssignableTo<IExceptionHandler>()).AsImplementedInterfaces().WithSingletonLifetime());

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreateCustomerCommand).Assembly));

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCors(corsPolicyName);

{
    var serviceScope = app.Services.CreateScope();

    var dbContext = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();

    dbContext.Database.Migrate();
}

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

var customerEndPoints = app.MapGroup("/api/v1/customers");

customerEndPoints.MapPost("/",
    async ([FromServices] IDispatcher dispatcher, [FromBody] CreateCustomerCommand command) =>
    {
        await dispatcher.ExecuteCommandAsync(command);
        return TypedResults.Created();
    });

customerEndPoints.MapGet("/{id:guid}", async (IDispatcher dispatcher, Guid id) =>
{
    var query = await dispatcher.ExecuteQueryAsync(new GetCustomerByIdQuery(id));
    return TypedResults.Ok(query);
});

app.Run();