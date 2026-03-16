using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.SharedModule.Enums;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

/// <summary>
/// Genel doküman/ek dosya tablosu - tüm modüllerde kullanılabilir
/// </summary>
public class Document : AuditableEntity
{
    // For EF
    public Document() { }

    public Guid Id { get; private set; }
    public int AttachmentType { get; private set; } // AttachmentTypeEnum: Invoice, Order, Party, Product vb.
    public Guid RelatedEntityId { get; private set; } // İlgili entity'nin ID'si
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string? ContentType { get; private set; }
    public long FileSize { get; private set; }
    public string? Description { get; private set; }

    public static Document Create(
        Guid id,
        int attachmentType,
        Guid relatedEntityId,
        string fileName,
        string filePath,
        string? contentType = null,
        long fileSize = 0,
        string? description = null)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.");
        if (relatedEntityId == Guid.Empty) throw new ArgumentException("RelatedEntityId cannot be empty.");
        if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("FileName is required.");
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("FilePath is required.");

        return new Document
        {
            Id = id,
            AttachmentType = attachmentType,
            RelatedEntityId = relatedEntityId,
            FileName = fileName.Trim(),
            FilePath = filePath.Trim(),
            ContentType = contentType,
            FileSize = fileSize,
            Description = description
        };
    }

    public void Update(string? description)
    {
        Description = description;
    }
}
