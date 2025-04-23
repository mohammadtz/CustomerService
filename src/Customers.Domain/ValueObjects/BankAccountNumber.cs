using Common.DomainBase;
using Customers.Domain.Exceptions;

namespace Customers.Domain.ValueObjects;

public record BankAccountNumber : BaseValueObject<string?>
{
    public BankAccountNumber(string? value) : base(Validate(value))
    {
    }

    private static string? Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        if (!value.All(char.IsDigit))
            throw new CannotContainNonDigitException();

        if (!LuhnAlgorithmIsValid(value))
            throw new BankAccountNumberFormatIsNotValid();

        return value;
    }

    public static bool LuhnAlgorithmIsValid(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || !number.All(char.IsDigit)) return false;

        int sum = 0;
        bool doubleDigit = false;

        for (int i = number.Length - 1; i >= 0; i--)
        {
            int digit = number[i] - '0';

            if (doubleDigit)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            doubleDigit = !doubleDigit;
        }

        return sum % 10 == 0;
    }
}