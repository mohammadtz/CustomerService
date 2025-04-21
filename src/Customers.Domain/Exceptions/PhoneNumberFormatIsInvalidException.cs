using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class PhoneNumberFormatIsInvalidException()
    : DomainException(ExceptionMessages.PhoneNumberFormatIsInvalidException);