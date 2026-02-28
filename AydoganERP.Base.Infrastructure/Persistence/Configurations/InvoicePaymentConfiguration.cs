using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using AydoganERP.Base.Domain.Modules.FinanceModule.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoicePaymentConfiguration : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoicePayments");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.PaymentDate)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 4);

        builder.Property(x => x.PaymentMethod)
            .IsRequired()
            .HasDefaultValue(PaymentMethodEnum.Cash);

        builder.Property(x => x.Reference)
            .HasMaxLength(100);

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        // Index
        builder.HasIndex(x => x.InvoiceId);
        builder.HasIndex(x => x.PaymentDate);
    }
}
