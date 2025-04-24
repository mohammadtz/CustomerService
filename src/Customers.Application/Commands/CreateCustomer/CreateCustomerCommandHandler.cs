using Common.CommandQueryBase;
using Common.InfrastructureBase;
using Customers.Domain;
using Customers.Domain.Contracts;
using Customers.Domain.Services;

namespace Customers.Application.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    ICustomerBaseInfoDuplicationChecker customerBaseInfoDuplicationChecker,
    IPhoneNumberValidator phoneNumberValidator,
    IEmailDuplicationChecker emailDuplicationChecker,
    IEmailFormatChecker emailFormatChecker) : ICommandHandler<CreateCustomerCommand>
{
    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(customerBaseInfoDuplicationChecker, phoneNumberValidator,
            emailDuplicationChecker, emailFormatChecker, request.FirstName, request.LastName, request.DateOfBirth,
            request.PhoneNumber, request.Email, request.BankAccountNumber);
        
        customerRepository.Create(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}