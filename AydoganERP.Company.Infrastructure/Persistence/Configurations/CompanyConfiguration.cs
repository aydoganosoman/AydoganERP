using AydoganERP.Base.Domain.Modules.CompanyModule.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Company.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.CompanyModule.Entities.Company>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.CompanyModule.Entities.Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Companies");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(CompanyStatusEnum.Active);

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Company)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.SetNull);
        
    }
}
