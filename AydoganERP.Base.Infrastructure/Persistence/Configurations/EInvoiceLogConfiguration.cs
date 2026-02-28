using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class EInvoiceLogConfiguration : IEntityTypeConfiguration<EInvoiceLog>
{
    public void Configure(EntityTypeBuilder<EInvoiceLog> builder)
    {
        builder.ToTable("EInvoiceLogs");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EInvoiceUUID)
            .HasMaxLength(50);

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.ProviderName)
            .HasMaxLength(100);

        builder.Property(e => e.ErrorMessage)
            .HasMaxLength(2000);

        builder.Property(e => e.PdfPath)
            .HasMaxLength(500);

        // UBL XML için text alanı
        builder.Property(e => e.UblXmlContent)
            .HasColumnType("text");

        builder.Property(e => e.ProviderResponse)
            .HasColumnType("text");

        // Invoice ile ilişki
        builder.HasOne(e => e.Invoice)
            .WithMany()
            .HasForeignKey(e => e.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(e => e.InvoiceId);
        builder.HasIndex(e => e.EInvoiceUUID);
        builder.HasIndex(e => e.Status);
    }
}
