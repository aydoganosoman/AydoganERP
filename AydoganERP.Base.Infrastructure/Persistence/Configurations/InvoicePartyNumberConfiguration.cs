using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoicePartyNumberConfiguration : IEntityTypeConfiguration<InvoicePartyNumber>
{
    public void Configure(EntityTypeBuilder<InvoicePartyNumber> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoicePartyNumbers");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.IsBuyer)
            .IsRequired();

        builder.Property(x => x.NumberType)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(200);

        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => new { x.InvoiceId, x.IsBuyer, x.NumberType });
    }
}
