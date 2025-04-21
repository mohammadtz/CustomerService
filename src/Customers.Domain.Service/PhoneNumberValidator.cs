using Customers.Domain.Customers.Services;
using PhoneNumbers;

namespace Customers.Domain.Service;

public class PhoneNumberValidator : IPhoneNumberValidator
{
    public bool Validate(string phoneNumber)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        var numberProto = phoneUtil.Parse(phoneNumber, "IR");
        return phoneUtil.IsValidNumber(numberProto);
    }
}