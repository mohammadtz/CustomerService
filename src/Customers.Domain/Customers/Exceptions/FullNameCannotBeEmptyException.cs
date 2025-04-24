using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Customers.Exceptions;

public class FullNameCannotBeEmptyException() : DomainException(ExceptionMessages.FullNameCannotBeEmptyException);