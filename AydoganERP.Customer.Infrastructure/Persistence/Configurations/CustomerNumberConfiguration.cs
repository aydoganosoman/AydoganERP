using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerNumberConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CustomerModule.Entities.CustomerNumber>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CustomerModule.Entities.CustomerNumber> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
