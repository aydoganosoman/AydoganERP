using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Inventory.Infrastructure.Persistence.Configurations;

public class ProductSupplierConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.InventoryModule.Entities.ProductSupplier>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.InventoryModule.Entities.ProductSupplier> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
