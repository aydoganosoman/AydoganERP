using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class IntegrationDefaultsConfiguration : IEntityTypeConfiguration<IntegrationDefaults>
{
    public void Configure(EntityTypeBuilder<IntegrationDefaults> builder)
    {
        builder.ToTable("IntegrationDefaults");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IntegrationId)
            .IsRequired();

        // Sipariş Ayarları
        builder.Property(x => x.ConsiderOrderStatuses)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.OrderStatuses)
            .HasMaxLength(1000);

        builder.Property(x => x.AutoCreateBarcode)
            .IsRequired()
            .HasDefaultValue(false);

        // Fatura Bilgileri
        builder.Property(x => x.DefaultVatRate)
            .IsRequired()
            .HasPrecision(5, 2)
            .HasDefaultValue(0);

        builder.Property(x => x.VatExemptionCode)
            .HasMaxLength(50);

        builder.Property(x => x.ExportVatExemptionCode)
            .HasMaxLength(50);

        // Sipariş Aktarım Ayarları
        builder.Property(x => x.OrderFilterDaysBefore)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.LastSyncStatus)
            .HasMaxLength(500);

        // Unique index on IntegrationId (1-1 relationship)
        builder.HasIndex(x => x.IntegrationId)
            .IsUnique();
    }
}
