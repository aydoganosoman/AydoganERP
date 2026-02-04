using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class City : Entity
{
    // For EF
    public City() { }

    private City(int id,
        int countryId,
        string name)
    {
        this.Id = id;
        this.CountryId = countryId;
        this.Name = name;
        this.Districts = new List<District>();
    }

    public int Id { get; private set; }
    public int CountryId { get; private set; }
    public Country Country { get; private set; }
    public string Name { get; private set; }

    public virtual List<District> Districts { get; set; }
    
    public static City Create(int id,
        int countryId,
        string name)
    {
        var _city = new City(id, countryId, name);

        return _city;
    }
}
