using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class CannotContainNonDigitException() : DomainException(ExceptionMessages.CannotContainNonDigitException);