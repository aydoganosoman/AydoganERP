using AydoganERP.Base.Domain.Modules.SharedModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AydoganERP.Base.Infrastructure.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Documents");

        builder.Property(x => x.AttachmentType)
            .IsRequired();

        builder.Property(x => x.RelatedEntityId)
            .IsRequired();

        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.FilePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ContentType)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // Indexes
        builder.HasIndex(x => x.AttachmentType);
        builder.HasIndex(x => x.RelatedEntityId);
        builder.HasIndex(x => new { x.AttachmentType, x.RelatedEntityId });
    }
}
