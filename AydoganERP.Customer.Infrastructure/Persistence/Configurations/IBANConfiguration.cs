using AydoganERP.Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class IBANConfiguration : IEntityTypeConfiguration<IBAN>
{
    public void Configure(EntityTypeBuilder<IBAN> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
