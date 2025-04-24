namespace Customers.Domain.Customers.ValueObjects;

public record CustomerBasicInfo(string FirstName, string LastName, DateOnly DateOfBirth);