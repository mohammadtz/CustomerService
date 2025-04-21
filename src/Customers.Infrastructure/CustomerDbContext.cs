using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
    
}