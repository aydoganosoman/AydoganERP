using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class EInvoiceIntegrationConfiguration : IEntityTypeConfiguration<EInvoiceIntegration>
{
    public void Configure(EntityTypeBuilder<EInvoiceIntegration> builder)
    {
        builder.ToTable("EInvoiceIntegrations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.Property(x => x.IntegrationType)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Index for filtering by company and type
        builder.HasIndex(x => new { x.CompanyId, x.IntegrationType });

    }
}
