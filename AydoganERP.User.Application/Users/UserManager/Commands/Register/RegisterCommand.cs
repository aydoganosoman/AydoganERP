using MediatR;

namespace AydoganERP.User.Application.Users.UserManager.Commands.Register;

public class RegisterCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
}
