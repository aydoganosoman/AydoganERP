using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Events.UserModule.Shared;

namespace AydoganERP.Base.Domain.Entities;

public class City : Entity<int>
{
    public City()
    {

    }

    public City(int id)
    {
        this.Id = id;
    }

    private City(int id,
        string name)
    {
        this.Id = id;
        this.Name = name;
        this.Towns = new List<Town>();
    }

    public string Name { get; private set; }

    public virtual List<Town> Towns { get; set; }

    public void AddTown(Town town)
    {
        this.Towns.Add(town);
    }

    public void UpdateFromIntegratedHandler(string name)
    {
        this.Name = name;
    }

    public static City Create(int id,
    string name)
    {
        var _city = new City(id, name);

        _city.PublishEvent(new CityCreatedEvent(_city.Id, _city.Name));

        return _city;
    }

    public static City CreateFromIntegratedHandler(int id,
        string name)
    {
        return new City(id, name);
    }

}
