using Customers.Domain;
using Customers.Domain.Services;
using NSubstitute;

namespace Customers.Tests.Unit.Domain.Base;

public abstract class CustomerTestBase
{
    protected const string Email = "john.doe@test.com";
    protected const string PhoneNumber = "+989129121212";

    protected readonly IPhoneNumberValidator PhoneNumberValidator = Substitute.For<IPhoneNumberValidator>();
    protected readonly IEmailDuplicationChecker EmailDuplicationChecker = Substitute.For<IEmailDuplicationChecker>();
    protected readonly IEmailFormatChecker EmailFormatChecker = Substitute.For<IEmailFormatChecker>();

    protected Customer InstantiateWithInvalidPhoneNumberCustomer(string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string? phoneNumber = "+989129121122", string? email = Email)
    {
        PhoneNumberValidator.Validate(phoneNumber!).Returns(false);

        return Customer.Create(PhoneNumberValidator, EmailDuplicationChecker, EmailFormatChecker, firstName, lastName,
            dateOfBirth: dateOfBirth ?? new DateOnly(2000, 1, 1), phoneNumber, email);
    }

    protected Customer InstantiateWithDuplicateEmailCustomer(string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string? phoneNumber = "+989129121122", string? email = Email)
    {
        PhoneNumberValidator.Validate(phoneNumber!).Returns(true);
        EmailFormatChecker.IsValid(email!).Returns(true);
        EmailDuplicationChecker.IsDuplicate(email!, Arg.Any<Guid[]>()).Returns(true);

        return Customer.Create(PhoneNumberValidator, EmailDuplicationChecker, EmailFormatChecker, firstName, lastName,
            dateOfBirth: dateOfBirth ?? new DateOnly(2000, 1, 1), phoneNumber, email);
    }

    protected Customer InstantiateWithInvalidFormatEmailCustomer(string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string? phoneNumber = "+989129121122", string? email = Email)
    {
        PhoneNumberValidator.Validate(phoneNumber!).Returns(true);
        EmailDuplicationChecker.IsDuplicate(email!).Returns(true);
        EmailFormatChecker.IsValid(email!).Returns(false);

        return Customer.Create(PhoneNumberValidator, EmailDuplicationChecker, EmailFormatChecker, firstName, lastName,
            dateOfBirth: dateOfBirth ?? new DateOnly(2000, 1, 1), phoneNumber, email);
    }

    protected Customer InstantiateValidCustomer(string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string? phoneNumber = "+989129121122", string? email = Email)
    {
        PhoneNumberValidator.Validate(phoneNumber!).Returns(true);
        EmailDuplicationChecker.IsDuplicate(phoneNumber!).Returns(false);
        EmailFormatChecker.IsValid(email!).Returns(true);

        return Customer.Create(PhoneNumberValidator, EmailDuplicationChecker, EmailFormatChecker, firstName, lastName,
            dateOfBirth: dateOfBirth ?? new DateOnly(2000, 1, 1), phoneNumber, email);
    }
}