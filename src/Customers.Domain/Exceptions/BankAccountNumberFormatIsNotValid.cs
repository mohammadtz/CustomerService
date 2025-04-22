using Common.ExceptionBase;
using Common.Resources;

namespace Customers.Domain.Exceptions;

public class BankAccountNumberFormatIsNotValid() : DomainException(ExceptionMessages.BankAccountNumberFormatIsNotValid);