using Common.Resources;
using Customers.Domain.Customers;
using Customers.Domain.Customers.Exceptions;
using Customers.Domain.Customers.Services;
using NSubstitute;
using Shouldly;

namespace Customers.Tests.Unit.Domain;

public class CustomerTests
{
    private readonly IPhoneNumberValidator phoneNumberValidator = Substitute.For<IPhoneNumberValidator>();

    [Fact]
    public void customer_created_when_customer_create_method_invoke()
    {
        var customer = InstantiateCustomer();

        customer.ShouldBeOfType<Customer>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public void full_name_cannot_be_null_or_white_space(string? firstname, string? lastname)
    {
        var customer = () => InstantiateCustomer(firstname!, lastname!);

        customer.ShouldThrow<FullNameCannotBeEmptyException>().Message
            .ShouldBe(ExceptionMessages.FullNameCannotBeEmptyException);
    }

    [Fact]
    public void full_name_filled_as_expected_values()
    {
        const string firstName = "Dean";
        const string lastName = "Winchester";

        var customer = InstantiateCustomer(firstName, lastName);

        customer.FirstName.ShouldBe(firstName);
        customer.LastName.ShouldBe(lastName);
    }

    [Fact]
    public void date_of_birth_date_cannot_be_default_value()
    {
        var customer = () => InstantiateCustomer(dateOfBirth: DateOnly.MinValue);

        customer.ShouldThrow<DateOfBirthDateIsRequiredException>();
    }

    [Fact]
    public void date_of_birth_date_filled_as_expected_value()
    {
        var dateOfBirth = new DateOnly(2010, 1, 1);

        var customer = InstantiateCustomer(dateOfBirth: dateOfBirth);

        customer.DateOfBirth.ShouldBe(dateOfBirth);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void phone_number_cannot_be_null_or_white_space(string? phoneNumber)
    {
        var customer = () => InstantiateCustomer(phoneNumber: phoneNumber!);

        customer.ShouldThrow<PhoneNumberCannotBeEmptyException>();
    }

    [Fact]
    public void phone_number_format_cannot_be_invalid()
    {
        const string phoneNumber = "04968465";
        phoneNumberValidator.Validate(phoneNumber).Returns(false);

        var customer = () => PureInstantiateCustomer(phoneNumberValidator, phoneNumber: phoneNumber);

        customer.ShouldThrow<PhoneNumberFormatIsInvalidException>();
    }

    private static Customer PureInstantiateCustomer(IPhoneNumberValidator phoneNumberValidator,
        string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string phoneNumber = "+989129121122")
    {
        return Customer.Create(phoneNumberValidator, firstName, lastName,
            dateOfBirth: dateOfBirth ?? new DateOnly(2000, 1, 1), phoneNumber);
    }

    private Customer InstantiateCustomer(string firstName = "John", string lastName = "Doe",
        DateOnly? dateOfBirth = null, string phoneNumber = "+989129121122")
    {
        phoneNumberValidator.Validate(phoneNumber).Returns(true);

        return PureInstantiateCustomer(phoneNumberValidator, firstName, lastName, dateOfBirth, phoneNumber);
    }
}