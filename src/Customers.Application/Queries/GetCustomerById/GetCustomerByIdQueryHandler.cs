using Common.CommandQueryBase;
using Common.ExceptionBase;
using Customers.Domain.Customers.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Customers.Application.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(IQueryDbContext dbContext)
    : IQueryHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
{
    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await dbContext.Customers
            .Where(x => x.Id == request.Id)
            .Select(x => new GetCustomerByIdResponse(x))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (customer == null) throw new DataNotFoundException();

        return customer;
    }
}