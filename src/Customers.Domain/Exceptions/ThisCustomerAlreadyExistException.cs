using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class ThisCustomerAlreadyExistException() : DomainException(ExceptionMessages.ThisCustomerAlreadyExistException);