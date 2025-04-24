using Common.DomainBase;
using Customers.Domain.Customers.DomainEvents;
using Customers.Domain.Customers.Exceptions;
using Customers.Domain.Customers.Services;
using Customers.Domain.Customers.ValueObjects;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace Customers.Domain.Customers;

public class Customer : AggregateRoot
{
    private Customer(ICustomerBaseInfoDuplicationChecker customerBaseInfoDuplicationChecker,
        IPhoneNumberValidator phoneNumberValidator, IEmailDuplicationChecker emailDuplicationChecker,
        IEmailFormatChecker emailFormatChecker, string firstName, string lastName, DateOnly dateOfBirth,
        string? phoneNumber, string? email, string? bankAccountNumber)
    {
        SetCustomerBaseInfo(customerBaseInfoDuplicationChecker, firstName, lastName, dateOfBirth);
        SetPhoneNumber(phoneNumberValidator, phoneNumber);
        SetEmail(emailDuplicationChecker, emailFormatChecker, email);
        SetBankAccountNumber(bankAccountNumber);
    }

    private Customer()
    {
    }

    public CustomerBasicInfo BasicInfo { get; set; }
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
        if (string.IsNullOrWhiteSpace(phoneNumber)) return;

        if (!phoneNumberValidator.Validate(phoneNumber))
            throw new PhoneNumberFormatIsInvalidException();

        PhoneNumber = phoneNumber;
    }

    private void SetCustomerBaseInfo(ICustomerBaseInfoDuplicationChecker customerBaseInfoDuplicationChecker,
        string firstName, string lastName, DateOnly dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new FullNameCannotBeEmptyException();

        if (dateOfBirth == DateOnly.MinValue) throw new DateOfBirthDateIsRequiredException();

        if (customerBaseInfoDuplicationChecker.IsDuplicate(firstName, lastName, dateOfBirth))
            throw new ThisCustomerAlreadyExistException();

        BasicInfo = new CustomerBasicInfo(firstName, lastName, dateOfBirth);
    }

    public static Customer Create(ICustomerBaseInfoDuplicationChecker customerBaseInfoDuplicationChecker,
        IPhoneNumberValidator phoneNumberValidator, IEmailDuplicationChecker emailDuplicationChecker,
        IEmailFormatChecker emailFormatChecker, string firstName, string lastName, DateOnly dateOfBirth,
        string? phoneNumber, string? email, string? bankAccountNumber)
    {
        var customer = new Customer(customerBaseInfoDuplicationChecker, phoneNumberValidator, emailDuplicationChecker,
            emailFormatChecker, firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);

        customer.AddDomainEvent(new CustomerCreatedEvent(customer.Id, firstName, lastName));

        return customer;
    }
}