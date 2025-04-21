using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class DateOfBirthDateIsRequiredException()
    : DomainException(ExceptionMessages.DateOfBirthDateIsRequiredException);