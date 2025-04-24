using Common.CommandQueryBase;
using Customers.Api.Configs;
using Customers.Api.Dependencies;
using Customers.Application.Commands.CreateCustomer;
using Customers.Application.Queries.GetCustomerById;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabasesConfig(builder.Configuration);
builder.Services.AddCorsConfig();
builder.Services.AddCommandQueryRegistrations();
builder.Services.AddAppServices();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddMassTransitConfig(builder.Configuration);

var app = builder.Build();

app.UseCors(RegisterCorsConfig.CorsPolicy);

app.ApplyMigrations();

app.UseSwagger();

app.UseSwaggerUI();

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