using AydoganERP.Base.Application.Interfaces;
using System.Security.Claims;

namespace AydoganERP.User.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserApiKey => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

    public Guid WorkingCompanyId => new Guid(_httpContextAccessor.HttpContext?.Request?.Headers["WorkingCompany"].ToString());

    public string Ip => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
}
