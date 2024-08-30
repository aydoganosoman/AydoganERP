using AydoganERP.Base.Application.Interfaces;
using System.Security.Claims;

namespace AydoganERP.Customer.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserApiKey => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public Guid WorkingCompanyId => new Guid(_httpContextAccessor.HttpContext?.Request?.Headers["WorkingCompany"].ToString());

    public string Ip => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
}
