using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Inventory.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.InventoryModule.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.InventoryModule.Entities.Product> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
