using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoiceOkcInfoConfiguration : IEntityTypeConfiguration<InvoiceOkcInfo>
{
    public void Configure(EntityTypeBuilder<InvoiceOkcInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoiceOkcInfos");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.FisNo)
            .HasMaxLength(50);

        builder.Property(x => x.ZReportNo)
            .HasMaxLength(50);

        builder.Property(x => x.OkcSerialNo)
            .HasMaxLength(50);

        builder.HasIndex(x => x.InvoiceId)
            .IsUnique(); // 1-1 ilişki için unique index
    }
}
