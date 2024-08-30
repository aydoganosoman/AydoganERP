using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Events.UserModule.Shared;

namespace AydoganERP.Base.Domain.Entities;

public class Category : Entity<Guid>
{
    private Category(Guid id,
        Guid companyId,
        string name,
        string color)
    {
        this.Id = id;
        this.CompanyId = companyId;
        this.Name = name;
        this.Color = color;
    }

    private Category(Guid companyId,
        string name,
        string color)
    {
        this.Id = Guid.NewGuid();
        this.CompanyId = companyId;
        this.Name = name;
        this.Color = color;

        this.PublishEvent(new CategoryCreatedEvent(this.Id, this.CompanyId, this.Name, this.Color));
    }

    public Guid CompanyId { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }

    public static Category Create(Guid companyId,
        string name,
        string color)
    {
        return new Category(companyId, name, color);
    }

    public static Category CreateFromIntegratedHandler(Guid id,
        Guid companyId,
        string name,
        string color)
    {
        return new Category(companyId, name, color);
    }

    public void UpdateFromIntegratedHandler(string name, string color)
    {
        this.Name = name;
        this.Color = color;
    }

    public void Update(string name, string color)
    {
        this.Name = name;
        this.Color = color;

        this.PublishEvent(new CategoryUpdatedEvent(this.Id, this.CompanyId, this.Name, this.Color));
    }
}
