using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;

namespace AydoganERP.Base.Domain.Modules.IdentityModule.Events;

public class UserCreatedEvent : IDomainEvent
{
    public User User { get; set; }

    public UserCreatedEvent(User user)
    {
        this.User = user;
    }
}