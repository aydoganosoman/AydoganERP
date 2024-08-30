using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.UpdateRefreshToken;

public class UpdateRefreshTokenCommand : IRequest
{
    public string ApiKey { get; set; }
    public string RefreshToken { get; set; }
}
