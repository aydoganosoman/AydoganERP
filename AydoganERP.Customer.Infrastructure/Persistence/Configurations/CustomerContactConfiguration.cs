using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerContactConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CustomerModule.Entities.CustomerContact>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CustomerModule.Entities.CustomerContact> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
