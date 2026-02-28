using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoiceLines");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.LineNumber)
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.ProductCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.UnitName)
            .HasMaxLength(50);

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 4);

        builder.Property(x => x.LineTotal)
            .HasPrecision(18, 4);

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(18, 4);

        builder.Property(x => x.VatAmount)
            .HasPrecision(18, 4);

        builder.Property(x => x.LineTotalWithVat)
            .HasPrecision(18, 4);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasOne(x => x.SerialNumber)
            .WithMany()
            .HasForeignKey(x => x.SerialNumberId)
            .OnDelete(DeleteBehavior.SetNull);

        // Index
        builder.HasIndex(x => new { x.InvoiceId, x.LineNumber });
    }
}
