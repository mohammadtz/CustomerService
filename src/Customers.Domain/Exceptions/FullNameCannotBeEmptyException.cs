using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class FullNameCannotBeEmptyException() : DomainException(ExceptionMessages.FullNameCannotBeEmptyException);