using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class EmailFormatIsNotValidException() : DomainException(ExceptionMessages.EmailFormatIsNotValidException);