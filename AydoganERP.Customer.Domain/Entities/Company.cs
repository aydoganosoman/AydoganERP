using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Customer.Domain.Entities;

public class Company : Entity<Guid>
{
    public Company()
    {
        
    }

    public Company(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public string Name { get; private set; }

    public void Update(string name)
    {
        this.Name = name;
    }
}
