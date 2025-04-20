using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Customers.Exceptions;

public class PhoneNumberFormatIsInvalidException()
    : DomainException(ExceptionMessages.PhoneNumberFormatIsInvalidException);