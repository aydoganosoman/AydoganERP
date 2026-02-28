using AydoganERP.Base.Domain.Common;

namespace AydoganERP.Base.Domain.Modules.SharedModule.Entities;

public class Currency : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
}