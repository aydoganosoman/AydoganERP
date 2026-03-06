using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class ProductUnitPriceConfiguration : IEntityTypeConfiguration<ProductUnitPrice>
{
    public void Configure(EntityTypeBuilder<ProductUnitPrice> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("ProductUnitPrices");

        builder.HasOne(x => x.Product)
            .WithMany(p => p.UnitPrices)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Unit)
            .WithMany()
            .HasForeignKey(x => x.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.ConversionRate)
            .HasPrecision(18, 6)
            .HasDefaultValue(1m);

        builder.Property(x => x.Barcode)
            .HasMaxLength(50);

        builder.Property(x => x.SaleUnitPrice)
            .HasPrecision(18, 4);

        builder.Property(x => x.SaleVatRate)
            .HasDefaultValue(0f);

        builder.Property(x => x.IsBaseUnit)
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        // Barkod unique index (aynı barkod birden fazla üründe olamaz)
        builder.HasIndex(x => x.Barcode)
            .IsUnique()
            .HasFilter("\"Barcode\" IS NOT NULL");

        // Product + Unit unique (bir ürün için aynı birim birden fazla tanımlanamaz)
        builder.HasIndex(x => new { x.ProductId, x.UnitId })
            .IsUnique();
    }
}
