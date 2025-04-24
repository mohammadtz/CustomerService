using Customers.Domain;
using Customers.Domain.Customers;
using Customers.Domain.Customers.Services;
using NSubstitute;

namespace Customers.Tests.Unit.Domain.Base;

public class CustomerTestBuilder
{
    private string defaultFirstName = "John";
    private string defaultLastName = "Doe";
    private DateOnly defaultDateOfBirth = new(2000, 1, 1);
    private string? phoneNumber = "+989129121122";
    private string? email = "john.doe@test.com";
    private string? bankAccountNumber = "79927398713";

    private readonly IPhoneNumberValidator phoneNumberValidator = Substitute.For<IPhoneNumberValidator>();
    private readonly IEmailDuplicationChecker emailDuplicationChecker = Substitute.For<IEmailDuplicationChecker>();
    private readonly IEmailFormatChecker emailFormatChecker = Substitute.For<IEmailFormatChecker>();

    private readonly ICustomerBaseInfoDuplicationChecker customerBaseInfoDuplicationChecker =
        Substitute.For<ICustomerBaseInfoDuplicationChecker>();

    public CustomerTestBuilder()
    {
        phoneNumberValidator.Validate(Arg.Any<string>()).Returns(true);
        emailFormatChecker.IsValid(Arg.Any<string>()).Returns(true);
        emailDuplicationChecker.IsDuplicate(Arg.Any<string>(), Arg.Any<Guid[]>()).Returns(false);
    }

    public CustomerTestBuilder WithInvalidPhoneNumber()
    {
        phoneNumberValidator.Validate(phoneNumber!).Returns(false);
        return this;
    }
    
    public CustomerTestBuilder WithDuplicateEmail()
    {
        emailFormatChecker.IsValid(email!).Returns(true);
        emailDuplicationChecker.IsDuplicate(email!, Arg.Any<Guid[]>()).Returns(true);
        return this;
    }

    public CustomerTestBuilder WithInvalidEmailFormat()
    {
        emailFormatChecker.IsValid(email!).Returns(false);
        return this;
    }

    public CustomerTestBuilder WithDuplicateBasicInfo()
    {
        customerBaseInfoDuplicationChecker.IsDuplicate(defaultFirstName, defaultLastName, defaultDateOfBirth).Returns(true);
        return this;
    }
    
    public CustomerTestBuilder WithEmail(string? email)
    {
        this.email = email;
        return this;
    }

    public CustomerTestBuilder WithPhoneNumber(string? phoneNumber)
    {
        this.phoneNumber = phoneNumber;
        return this;
    }

    public CustomerTestBuilder WithBankAccount(string? account)
    {
        bankAccountNumber = account;
        return this;
    }

    public CustomerTestBuilder WithBasicInfo(string firstname, string lastname, DateOnly dateOfBirth)
    {
        defaultFirstName = firstname;
        defaultLastName = lastname;
        defaultDateOfBirth = dateOfBirth;
        return this;
    }

    public Customer Build()
    {
        return Customer.Create(
            customerBaseInfoDuplicationChecker,
            phoneNumberValidator,
            emailDuplicationChecker,
            emailFormatChecker,
            defaultFirstName,
            defaultLastName,
            defaultDateOfBirth,
            phoneNumber,
            email,
            bankAccountNumber);
    }
}