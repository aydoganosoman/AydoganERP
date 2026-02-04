using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Company.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CompanyModule.Entities.Company>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CompanyModule.Entities.Company> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
