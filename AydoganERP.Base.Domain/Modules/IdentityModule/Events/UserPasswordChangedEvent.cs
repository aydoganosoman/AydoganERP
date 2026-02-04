using AydoganERP.Base.Domain.Common;
using AydoganERP.Base.Domain.Modules.IdentityModule.Entities;

namespace AydoganERP.Base.Domain.Modules.IdentityModule.Events;

public class UserPasswordChangedEvent : IDomainEvent
{
    public User User { get; set; }

    public UserPasswordChangedEvent(User user)
    {
        this.User = user;
    }
}