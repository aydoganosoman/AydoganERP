using AydoganERP.Base.Domain.Modules.InventoryModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Inventory.Infrastructure.Persistence.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.QuantityDelta)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.ReferenceType)
            .HasMaxLength(50);

        builder.Property(x => x.ReferenceNo)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasOne(x => x.Product)
            .WithMany(p => p.Movements)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ProductId);
        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => new { x.ReferenceType, x.ReferenceId });
    }
}
