using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Customer> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
