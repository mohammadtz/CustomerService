using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class EmailIsDuplicateException() : DomainException(ExceptionMessages.EmailIsDuplicateException);