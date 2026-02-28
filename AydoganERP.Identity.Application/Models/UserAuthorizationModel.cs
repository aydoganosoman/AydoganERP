namespace AydoganERP.Identity.Application.Models;

public record UserAuthModel(
    Guid Id,
    int Role,
    Guid? CompanyId,
    string Name,
    string Title,
    string Email,
    string ApiKey,
    int Status)
{
    public TokenModel Token { get; set; }
};