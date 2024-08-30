using FluentValidation;

namespace AydoganERP.User.Application.Users.UserManager.Commands.AddAccessibleUser;

public class AddAccessibleUserCommandValidator : AbstractValidator<AddAccessibleUserCommand>
{
    public AddAccessibleUserCommandValidator()
    {
        RuleFor(v => v.UserEmail)
            .EmailAddress().WithMessage("Geçerli bir eposta adresi giriniz.")
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
    }
}
