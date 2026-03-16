using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoicePaymentTermConfiguration : IEntityTypeConfiguration<InvoicePaymentTerm>
{
    public void Configure(EntityTypeBuilder<InvoicePaymentTerm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoicePaymentTerms");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.PaymentMethod)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 4);

        builder.Property(x => x.PenaltyRate)
            .HasPrecision(5, 2);

        builder.Property(x => x.PenaltyAmount)
            .HasPrecision(18, 4);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.HasIndex(x => x.InvoiceId);
    }
}
