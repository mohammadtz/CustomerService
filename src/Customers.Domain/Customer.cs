using Common.DomainBase;
using Customers.Domain.Exceptions;
using Customers.Domain.Services;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Customers.Domain;

public class Customer : BaseEntity
{
    private Customer(IPhoneNumberValidator phoneNumberValidator, IEmailDuplicationChecker emailDuplicationChecker,
        IEmailFormatChecker emailFormatChecker, string firstName, string lastName, DateOnly dateOfBirth,
        string? phoneNumber, string? email)
    {
        SetFullName(firstName, lastName);
        SetDateOfBirth(dateOfBirth);
        SetPhoneNumber(phoneNumberValidator, phoneNumber);
        SetEmail(emailDuplicationChecker, emailFormatChecker, email);
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }

    private void SetEmail(IEmailDuplicationChecker emailDuplicationChecker, IEmailFormatChecker emailFormatChecker,
        string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return;

        if (!emailFormatChecker.IsValid(email))
            throw new EmailFormatIsNotValidException();

        if (emailDuplicationChecker.IsDuplicate(email, Id))
            throw new EmailIsDuplicateException();

        Email = email;
    }

    private void SetPhoneNumber(IPhoneNumberValidator phoneNumberValidator, string? phoneNumber)
    {
        if (phoneNumber == null) return;

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

    public static Customer Create(IPhoneNumberValidator phoneNumberValidator,
        IEmailDuplicationChecker emailDuplicationChecker, IEmailFormatChecker emailFormatChecker, string firstName,
        string lastName,
        DateOnly dateOfBirth, string? phoneNumber, string? email)
    {
        var customer = new Customer(phoneNumberValidator, emailDuplicationChecker, emailFormatChecker, firstName,
            lastName, dateOfBirth,
            phoneNumber, email);

        return customer;
    }
}