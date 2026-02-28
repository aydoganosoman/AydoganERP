using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Inventory.Infrastructure.Persistence.Configurations;

public class StockBatchConfiguration : IEntityTypeConfiguration<Base.Domain.Modules.InventoryModule.Entities.StockBatch>
{
    public void Configure(EntityTypeBuilder<Base.Domain.Modules.InventoryModule.Entities.StockBatch> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
