using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerBankAccountConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CustomerModule.Entities.CustomerBankAccount>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CustomerModule.Entities.CustomerBankAccount> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
