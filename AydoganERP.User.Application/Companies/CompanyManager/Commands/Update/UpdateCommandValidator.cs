using AydoganERP.User.Application.Companies.CompanyManager.Commands.Update;
using FluentValidation;

namespace AydoganERP.User.Application.Users.UserManager.Commands.Register;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");
    }
}
