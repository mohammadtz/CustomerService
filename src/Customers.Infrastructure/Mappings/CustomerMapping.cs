using Customers.Domain;
using Customers.Domain.Customers;
using Customers.Domain.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Customers.Infrastructure.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        builder.Property(x => x.Email).HasMaxLength(200);

        builder.OwnsOne(x => x.BasicInfo, x =>
        {
            x.HasIndex(y => new { y.FirstName, y.LastName, y.DateOfBirth }).IsUnique();
            x.Property(y => y.FirstName).HasMaxLength(100);
            x.Property(y => y.LastName).HasMaxLength(100);
        });

        builder.HasIndex(x => x.Email).IsUnique();

        var bankAccountNumberConverter = new ValueConverter<BankAccountNumber, string?>(
            v => v.Value,
            v => new BankAccountNumber(v));

        builder.Property(x => x.BankAccountNumber).HasMaxLength(100).HasConversion(bankAccountNumberConverter!);
    }
}