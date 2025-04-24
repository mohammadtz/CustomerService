using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Customers.Exceptions;

public class EmailFormatIsNotValidException() : DomainException(ExceptionMessages.EmailFormatIsNotValidException);