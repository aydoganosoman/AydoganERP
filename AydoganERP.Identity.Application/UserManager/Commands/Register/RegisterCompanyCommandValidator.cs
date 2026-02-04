using FluentValidation;

namespace AydoganERP.Identity.Application.UserManager.Commands.Register;

public class RegisterCompanyCommandValidator : AbstractValidator<RegisterCompanyCommand>
{
    public RegisterCompanyCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

        RuleFor(v => v.Email)
            .EmailAddress().WithMessage("Geçerli bir eposta adresi giriniz.")
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

    }
}
