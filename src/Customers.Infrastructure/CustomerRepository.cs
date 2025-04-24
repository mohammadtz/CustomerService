using Common.InfrastructureBase;
using Customers.Domain;
using Customers.Domain.Contracts;

namespace Customers.Infrastructure;

public class CustomerRepository(CustomerDbContext dbContext) : BaseRepository<Customer>(dbContext), ICustomerRepository;