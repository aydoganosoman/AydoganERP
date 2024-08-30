using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.AddAccessibleUser;

public class AddAccessibleUserCommand : IRequest
{
    public string UserEmail { get; set; }
}
