using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class ECommerceIntegrationConfiguration : IEntityTypeConfiguration<ECommerceIntegration>
{
    public void Configure(EntityTypeBuilder<ECommerceIntegration> builder)
    {
        builder.ToTable("ECommerceIntegrations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.Property(x => x.IntegrationType)
            .IsRequired();

        builder.Property(x => x.StoreName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IntegrationUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Username)
            .HasMaxLength(200);

        builder.Property(x => x.Credentials)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Index for filtering by company and type
        builder.HasIndex(x => new { x.CompanyId, x.IntegrationType });

        // Navigation to Defaults (1-1)
        builder.HasOne(x => x.Defaults)
            .WithOne(x => x.Integration)
            .HasForeignKey<IntegrationDefaults>(x => x.IntegrationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
