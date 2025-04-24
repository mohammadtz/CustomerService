using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Customers.Exceptions;

public class EmailIsDuplicateException() : DomainException(ExceptionMessages.EmailIsDuplicateException);