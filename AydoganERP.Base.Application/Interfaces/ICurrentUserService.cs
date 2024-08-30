namespace AydoganERP.Base.Application.Interfaces;

public interface ICurrentUserService
{
    string Ip { get; }
    string UserApiKey { get; }
    Guid WorkingCompanyId { get; }
}
