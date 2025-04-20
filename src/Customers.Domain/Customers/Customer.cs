using Customers.Domain.Customers.Exceptions;
using Customers.Domain.Customers.Services;

namespace Customers.Domain.Customers;

public class Customer
{
    private Customer(IPhoneNumberValidator phoneNumberValidator, string firstName, string lastName,
        DateOnly dateOfBirth, string phoneNumber)
    {
        SetFullName(firstName, lastName);
        SetDateOfBirth(dateOfBirth);
        SetPhoneNumber(phoneNumberValidator, phoneNumber);
    }

    private void SetPhoneNumber(IPhoneNumberValidator phoneNumberValidator, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new PhoneNumberCannotBeEmptyException();

        if (!phoneNumberValidator.Validate(phoneNumber))
            throw new PhoneNumberFormatIsInvalidException();

        PhoneNumber = phoneNumber;
    }

    private void SetDateOfBirth(DateOnly dateOfBirth)
    {
        if (dateOfBirth == DateOnly.MinValue) throw new DateOfBirthDateIsRequiredException();

        DateOfBirth = dateOfBirth;
    }

    private void SetFullName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new FullNameCannotBeEmptyException();

        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; }

    public static Customer Create(IPhoneNumberValidator phoneNumberValidator, string firstName, string lastName,
        DateOnly dateOfBirth, string phoneNumber)
    {
        var customer = new Customer(phoneNumberValidator, firstName, lastName, dateOfBirth, phoneNumber);

        return customer;
    }
}