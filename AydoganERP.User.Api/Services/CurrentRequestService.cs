using AydoganERP.Base.Application.Interfaces;

namespace AydoganERP.User.Api.Services;

public class CurrentRequestService : ICurrentRequestService
{
    private IHttpContextAccessor _httpContextAccessor;
    public CurrentRequestService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string IP => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
}
