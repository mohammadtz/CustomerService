using Common.DomainBase;
using Customers.Domain.Exceptions;
using Customers.Domain.Services;
using Customers.Domain.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Customers.Domain;

public class Customer : BaseEntity
{
    private Customer(IPhoneNumberValidator phoneNumberValidator, IEmailDuplicationChecker emailDuplicationChecker,
        IEmailFormatChecker emailFormatChecker, string firstName, string lastName, DateOnly dateOfBirth,
        string? phoneNumber, string? email, string? bankAccountNumber)
    {
        SetFullNameAndDateOfBirth(firstName, lastName, dateOfBirth);
        SetPhoneNumber(phoneNumberValidator, phoneNumber);
        SetEmail(emailDuplicationChecker, emailFormatChecker, email);
        SetBankAccountNumber(bankAccountNumber);
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public DateOnly DateOfBirth { get; private set; }

    public string? PhoneNumber { get; private set; }

    public string? Email { get; private set; }
    public BankAccountNumber? BankAccountNumber { get; private set; }
    
    private void SetBankAccountNumber(string? bankAccountNumber)
    {
        BankAccountNumber = new BankAccountNumber(bankAccountNumber);
    }

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

    private void SetFullNameAndDateOfBirth(string firstName, string lastName, DateOnly dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new FullNameCannotBeEmptyException();
        
        if (dateOfBirth == DateOnly.MinValue) throw new DateOfBirthDateIsRequiredException();

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public static Customer Create(IPhoneNumberValidator phoneNumberValidator,
        IEmailDuplicationChecker emailDuplicationChecker, IEmailFormatChecker emailFormatChecker, string firstName,
        string lastName,
        DateOnly dateOfBirth, string? phoneNumber, string? email, string? bankAccountNumber)
    {
        var customer = new Customer(phoneNumberValidator, emailDuplicationChecker, emailFormatChecker, firstName,
            lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);

        return customer;
    }
}