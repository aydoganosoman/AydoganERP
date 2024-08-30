using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Events.UserModule.Shared;

namespace AydoganERP.Base.Domain.Entities;

public class Town : Entity<int>
{
    public Town()
    {

    }

    private Town(int id,
        City city,
        string name)
    {
        this.Id = id;
        this.City = city;
        this.Name = name;

    }

    public City City { get; private set; }
    public string Name { get; private set; }

    public void UpdateFromIntegratedHandler(string name)
    {
        this.Name = name;
    }

    public static Town Create(int id,
        City city,
        string name)
    {
        var _town = new Town(id, city, name);

        _town.PublishEvent(new TownCreatedEvent(_town.Id, _town.City.Id, _town.Name));

        return _town;
    }

    public static Town CreateFromIntegratedHandler(int id,
        City city,
        string name)
    {
        return new Town(id, city, name);
    }

}
