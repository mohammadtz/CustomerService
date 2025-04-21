using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class PhoneNumberCannotBeEmptyException() : DomainException(ExceptionMessages.PhoneNumberCannotBeEmptyException);