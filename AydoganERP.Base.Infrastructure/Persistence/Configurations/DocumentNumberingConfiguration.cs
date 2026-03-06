using AydoganERP.Base.Domain.Modules.CompanyModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class DocumentNumberingConfiguration : IEntityTypeConfiguration<DocumentNumbering>
{
    public void Configure(EntityTypeBuilder<DocumentNumbering> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("DocumentNumberings");

        builder.Property(x => x.CompanyId)
            .IsRequired();

        builder.HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.DocumentType)
            .IsRequired();

        builder.Property(x => x.Prefix)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.CurrentNumber)
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(x => x.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Unique index: CompanyId + DocumentType + Prefix
        builder.HasIndex(x => new { x.CompanyId, x.DocumentType, x.Prefix })
            .IsUnique();
    }
}
