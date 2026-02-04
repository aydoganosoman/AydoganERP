using AydoganERP.Base.Application.Common.Mappings;

namespace AydoganERP.Identity.Application.Models;

public class UserDto : IMapFrom<Base.Domain.Modules.IdentityModule.Entities.User>
{
    public Guid Id { get; set; }
    public Guid? CompanyId { get; set; }
    public int Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Title { get; set; }
    public string ApiKey { get; set; }
    public string? RefreshToken { get; set; }
    public bool ForcePasswordChange { get; set; }
    public int Status { get; set; }
}
