namespace Customers.Api.Options;

public class RabbitMqOptions
{
    public required string Url { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}