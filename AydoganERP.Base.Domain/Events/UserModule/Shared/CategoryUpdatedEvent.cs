using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.UserModule.Shared;

public class CategoryUpdatedEvent : IDomainEvent
{
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryColor { get; set; }

    public CategoryUpdatedEvent(Guid categoryId,
        Guid companyId,
        string categoryName,
        string categoryColor)
    {
        CategoryId = categoryId;
        CompanyId = companyId;
        CategoryName = categoryName;
        CategoryColor = categoryColor;
    }
}
