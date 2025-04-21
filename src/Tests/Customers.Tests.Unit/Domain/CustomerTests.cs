using Common.Resources;
using Customers.Domain;
using Customers.Domain.Exceptions;
using Customers.Tests.Unit.Domain.Base;
using Shouldly;

namespace Customers.Tests.Unit.Domain;

public class CustomerTests : CustomerTestBase
{
    [Fact]
    public void customer_created_when_customer_create_method_invoke()
    {
        var customer = InstantiateValidCustomer();

        customer.ShouldBeOfType<Customer>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public void full_name_cannot_be_null_or_white_space(string? firstname, string? lastname)
    {
        var customer = () => InstantiateValidCustomer(firstname!, lastname!);

        customer.ShouldThrow<FullNameCannotBeEmptyException>().Message
            .ShouldBe(ExceptionMessages.FullNameCannotBeEmptyException);
    }

    [Fact]
    public void full_name_filled_as_expected_values()
    {
        const string firstName = "Dean";
        const string lastName = "Winchester";

        var customer = InstantiateValidCustomer(firstName, lastName);

        customer.FirstName.ShouldBe(firstName);
        customer.LastName.ShouldBe(lastName);
    }

    [Fact]
    public void date_of_birth_date_cannot_be_default_value()
    {
        var customer = () => InstantiateValidCustomer(dateOfBirth: DateOnly.MinValue);

        customer.ShouldThrow<DateOfBirthDateIsRequiredException>();
    }

    [Fact]
    public void date_of_birth_date_filled_as_expected_value()
    {
        var dateOfBirth = new DateOnly(2010, 1, 1);

        var customer = InstantiateValidCustomer(dateOfBirth: dateOfBirth);

        customer.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Fact]
    public void phone_number_format_cannot_be_invalid()
    {
        var customer = () =>
            InstantiateWithInvalidPhoneNumberCustomer(phoneNumber: PhoneNumber);

        customer.ShouldThrow<PhoneNumberFormatIsInvalidException>();
    }

    [Fact]
    public void phone_number_filled_as_expected_value()
    {
        var customer = InstantiateValidCustomer(phoneNumber: PhoneNumber);

        customer.PhoneNumber.ShouldBe(PhoneNumber);
    }

    [Fact]
    public void phone_number_can_be_set_null()
    {
        var customer = InstantiateValidCustomer(phoneNumber: null);

        customer.PhoneNumber.ShouldBeNull();
    }

    [Fact]
    public void email_can_be_set_null()
    {
        var customer = InstantiateValidCustomer(email: null);

        customer.Email.ShouldBeNull();
    }

    [Fact]
    public void email_filled_as_expected_value()
    {
        const string email = "dean@winchester.com";

        var customer = InstantiateValidCustomer(email: email);

        customer.Email.ShouldBe(email);
    }

    [Fact]
    public void email_cannot_be_duplicated()
    {
        var customer = () => InstantiateWithDuplicateEmailCustomer();

        customer.ShouldThrow<EmailIsDuplicateException>();
    }

    [Fact]
    public void email_format_cannot_be_invalid()
    {
        var customer = () => InstantiateWithInvalidFormatEmailCustomer();

        customer.ShouldThrow<EmailFormatIsNotValidException>();
    }
}