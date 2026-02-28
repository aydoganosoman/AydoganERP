using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Customer.Infrastructure.Persistence.Configurations;

public class CustomerBranchConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CustomerModule.Entities.CustomerBranch>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CustomerModule.Entities.CustomerBranch> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
