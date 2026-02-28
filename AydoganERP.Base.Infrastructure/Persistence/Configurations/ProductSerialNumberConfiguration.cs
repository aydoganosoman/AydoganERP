using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class ProductSerialNumberConfiguration : IEntityTypeConfiguration<ProductSerialNumber>
{
    public void Configure(EntityTypeBuilder<ProductSerialNumber> builder)
    {
        builder.ToTable("ProductSerialNumbers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.SerialNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.SalePrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        // Product ilişkisi
        builder.HasOne(x => x.Product)
            .WithMany(p => p.SerialNumbers)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Alış müşterisi ilişkisi
        builder.HasOne(x => x.PurchaseCustomer)
            .WithMany()
            .HasForeignKey(x => x.PurchaseCustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Satış müşterisi ilişkisi
        builder.HasOne(x => x.SaleCustomer)
            .WithMany()
            .HasForeignKey(x => x.SaleCustomerId)
            .OnDelete(DeleteBehavior.SetNull);

        // Benzersizlik: Aynı ürün için aynı seri numarası olamaz
        builder.HasIndex(x => new { x.ProductId, x.SerialNumber }).IsUnique();
        builder.HasIndex(x => x.ProductId);
        builder.HasIndex(x => x.Status);
    }
}
