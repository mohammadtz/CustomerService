using Common.Resources;
using Customers.Domain;
using Customers.Domain.Exceptions;
using Customers.Domain.ValueObjects;
using Customers.Tests.Unit.Domain.Base;
using Shouldly;

namespace Customers.Tests.Unit.Domain;

public class CustomerTests
{
    private readonly DateOnly dateOfBirth = new(2010, 1, 1);
    private const string FirstName = "Dean";
    private const string LastName = "Winchester";
    private const string BankAccountNumber = "79927398713";

    [Fact]
    public void customer_created_when_customer_create_method_invoke()
    {
        var customer = new CustomerTestBuilder().Build();

        customer.ShouldBeOfType<Customer>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    [InlineData(null, "test")]
    [InlineData("test", null)]
    public void full_name_cannot_be_null_or_white_space(string? firstname, string? lastname)
    {
        var customer = () =>
            new CustomerTestBuilder().WithBasicInfo(firstname!, lastname!, dateOfBirth).Build();

        customer.ShouldThrow<FullNameCannotBeEmptyException>().Message
            .ShouldBe(ExceptionMessages.FullNameCannotBeEmptyException);
    }

    [Fact]
    public void basic_info_filled_as_expected_values()
    {
        var customer = new CustomerTestBuilder().WithBasicInfo(FirstName, LastName, dateOfBirth).Build();

        customer.BasicInfo.FirstName.ShouldBe(FirstName);
        customer.BasicInfo.LastName.ShouldBe(LastName);
        customer.BasicInfo.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Fact]
    public void date_of_birth_date_cannot_be_default_value()
    {
        var customer = () => new CustomerTestBuilder().WithBasicInfo(FirstName, LastName, default).Build();

        customer.ShouldThrow<DateOfBirthDateIsRequiredException>();
    }

    [Fact]
    public void customer_basic_info_cannot_be_duplicated()
    {
        var customer = () => new CustomerTestBuilder().WithDuplicateBasicInfo().Build();

        customer.ShouldThrow<ThisCustomerAlreadyExistException>();
    }
    
    [Fact]
    public void phone_number_format_cannot_be_invalid()
    {
        var customer = () => new CustomerTestBuilder().WithInvalidPhoneNumber().Build();

        customer.ShouldThrow<PhoneNumberFormatIsInvalidException>();
    }

    [Fact]
    public void phone_number_filled_as_expected_value()
    {
        const string phoneNumber = "09121112233";
        var customer = new CustomerTestBuilder().WithPhoneNumber(phoneNumber).Build();

        customer.PhoneNumber.ShouldBe(phoneNumber);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void phone_number_can_be_set_null(string? phoneNumber)
    {
        var customer = new CustomerTestBuilder().WithPhoneNumber(phoneNumber).Build();

        customer.PhoneNumber.ShouldBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void email_can_be_set_null_or_empty(string? email)
    {
        var customer = new CustomerTestBuilder().WithEmail(email).Build();

        customer.Email.ShouldBeNull();
    }

    [Fact]
    public void email_filled_as_expected_value()
    {
        const string email = "dean@winchester.com";

        var customer = new CustomerTestBuilder().WithEmail(email).Build();

        customer.Email.ShouldBe(email);
    }

    [Fact]
    public void email_cannot_be_duplicated()
    {
        var customer = () => new CustomerTestBuilder().WithDuplicateEmail().Build();

        customer.ShouldThrow<EmailIsDuplicateException>();
    }

    [Fact]
    public void email_format_cannot_be_invalid()
    {
        var customer = () => new CustomerTestBuilder().WithInvalidEmailFormat().Build();

        customer.ShouldThrow<EmailFormatIsNotValidException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void bank_account_number_can_be_null_or_empty(string? bankAccountNumber)
    {
        var customer = new CustomerTestBuilder().WithBankAccount(bankAccountNumber).Build();

        customer.BankAccountNumber?.Value.ShouldBeNull();
    }

    [Fact]
    public void bank_account_number_filled_as_expected_values()
    {
        var customer = new CustomerTestBuilder().WithBankAccount(BankAccountNumber).Build();

        customer.BankAccountNumber.ShouldBe(new BankAccountNumber(BankAccountNumber));
    }

    [Fact]
    public void bank_account_cannot_contain_non_digit()
    {
        var customer = () => new CustomerTestBuilder().WithBankAccount("abc").Build();

        customer.ShouldThrow<CannotContainNonDigitException>();
    }

    [Fact]
    public void bank_account_cannot_be_have_invalid_format()
    {
        var customer = () => new CustomerTestBuilder().WithBankAccount("123").Build();

        customer.ShouldThrow<BankAccountNumberFormatIsNotValid>();
    }
}