using Customers.Domain.Customers.Services;
using PhoneNumbers;

namespace Customers.Domain.Service;

public class PhoneNumberValidator : IPhoneNumberValidator
{
    private const string Region = "IR";

    public bool Validate(string phoneNumber)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();

        try
        {
            var numberProto = phoneUtil.Parse(phoneNumber, Region);

            if (!phoneUtil.IsValidNumberForRegion(numberProto, Region))
                return false;

            var numberType = phoneUtil.GetNumberType(numberProto);

            return numberType is PhoneNumberType.MOBILE or PhoneNumberType.FIXED_LINE_OR_MOBILE;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}