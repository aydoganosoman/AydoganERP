using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoiceOrderInfoConfiguration : IEntityTypeConfiguration<InvoiceOrderInfo>
{
    public void Configure(EntityTypeBuilder<InvoiceOrderInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoiceOrderInfos");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.OrderNumber)
            .HasMaxLength(50);

        builder.Property(x => x.WaybillNumber)
            .HasMaxLength(50);

        builder.Property(x => x.DocumentPath)
            .HasMaxLength(500);

        builder.Property(x => x.DocumentName)
            .HasMaxLength(200);

        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => x.OrderNumber);
        builder.HasIndex(x => x.WaybillNumber);
    }
}
