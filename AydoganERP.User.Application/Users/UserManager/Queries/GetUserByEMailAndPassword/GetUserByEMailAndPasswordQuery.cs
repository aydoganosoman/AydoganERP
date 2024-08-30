using AydoganERP.Base.Application.Models;
using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Queries.GetUserByEMailAndPassword;

public class GetUserByEMailAndPasswordQuery : IRequest<UserAuthModel>
{
    public string EMail { get; set; }
    public string Password { get; set; }
}
