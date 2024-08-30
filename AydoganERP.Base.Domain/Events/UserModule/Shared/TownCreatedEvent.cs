using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.UserModule.Shared;

public class TownCreatedEvent : IDomainEvent
{
    public int TownId { get; set; }
    public int CityId { get; set; }
    public string TownName { get; set; }

    public TownCreatedEvent(int townId,
        int cityId,
        string townName)
    {
        TownId = townId;
        CityId = cityId;
        TownName = townName;
    }
}
