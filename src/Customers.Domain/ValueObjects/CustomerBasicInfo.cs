namespace Customers.Domain.ValueObjects;

public record CustomerBasicInfo(string FirstName, string LastName, DateOnly DateOfBirth);