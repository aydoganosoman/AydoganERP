using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class District : Entity
{
    // For EF
    public District() { }

    private District(int id,
        int cityId,
        string name)
    {
        this.Id = id;
        this.CityId = cityId;
        this.Name = name;

    }

    public int Id { get; private set; }
    public int CityId { get; private set; }
    public City City { get; private set; }
    public string Name { get; private set; }

    public static District Create(int id,
        int cityId,
        string name)
    {
        var _town = new District(id, cityId, name);

        return _town;
    }

}
