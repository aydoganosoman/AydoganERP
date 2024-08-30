using FluentValidation;

namespace AydoganERP.User.Application.Users.UserManager.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

        RuleFor(v => v.LastName)
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

        RuleFor(v => v.Email)
            .EmailAddress().WithMessage("Geçerli bir eposta adresi giriniz.")
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

        RuleFor(v => v.Password)
            .MinimumLength(6).WithMessage("Şifre minimum 6 karakter olmalıdır.")
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.");

        RuleFor(v => v.PasswordConfirmation)
            .MinimumLength(6).WithMessage("Şifre minimum 6 karakter olmalıdır.")
            .NotNull().WithMessage("Bu alan boş bırakılamaz.")
            .NotEmpty().WithMessage("Bu alan boş bırakılamaz.")
            .Equal(v => v.Password).WithMessage("Şifreler uyuşmuyor.");

    }
}
