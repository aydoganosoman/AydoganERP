using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Inventory.Infrastructure.Persistence.Configurations;

public class ProductBarcodeConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.InventoryModule.Entities.ProductBarcode>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.InventoryModule.Entities.ProductBarcode> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
