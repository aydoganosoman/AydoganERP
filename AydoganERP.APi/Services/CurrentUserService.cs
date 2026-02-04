using AydoganERP.Base.Application.Common.Interfaces;
using System.Security.Claims;

namespace AydoganERP.APi.Services;

public class CurrentUserService : ICurrentUserService
{
    private IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Ip => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
    public string UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirstValue("email");
}
