using AydoganERP.Base.Domain.Modules.FinanceModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class InvoiceNoteConfiguration : IEntityTypeConfiguration<InvoiceNote>
{
    public void Configure(EntityTypeBuilder<InvoiceNote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("InvoiceNotes");

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.NoteText)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.SortOrder)
            .HasDefaultValue(0);

        builder.HasIndex(x => x.InvoiceId);
    }
}
