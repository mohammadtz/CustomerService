using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Customers.Exceptions;

public class CannotContainNonDigitException() : DomainException(ExceptionMessages.CannotContainNonDigitException);