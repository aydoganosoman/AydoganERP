using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Events.UserModule.Shared;

public class CityCreatedEvent : IDomainEvent
{
    public int CityId { get; set; }
    public string CityName { get; set; }

    public CityCreatedEvent(int cityId,
        string cityName)
    {
        CityId = cityId;
        CityName = cityName;
    }
}
