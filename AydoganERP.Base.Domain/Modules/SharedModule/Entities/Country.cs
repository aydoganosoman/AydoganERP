using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class Country : Entity
{
    // For EF
    public Country() { }

    private Country(int id,
        string name)
    {
        this.Id = id;
        this.Name = name;
        this.Cities = new List<City>();
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

    public virtual List<City> Cities { get; set; }
    
    public static Country Create(int id,
        string name)
    {
        var _country = new Country(id, name);

        return _country;
    }
}
