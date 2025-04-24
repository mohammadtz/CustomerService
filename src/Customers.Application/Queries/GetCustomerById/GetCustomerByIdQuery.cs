using Common.CommandQueryBase;

namespace Customers.Application.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IQuery<GetCustomerByIdResponse>;