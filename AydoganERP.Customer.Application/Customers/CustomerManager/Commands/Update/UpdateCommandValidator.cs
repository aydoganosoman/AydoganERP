using FluentValidation;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Müşteri adı boş bırakılamaz.")
            .NotNull().WithMessage("Müşteri adı boş bırakılamaz.");

        RuleFor(v => v.Phone)
            .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$").WithMessage("Geçerli bir telefon bilgisi giriniz.");

        RuleFor(v => v.EMail)
            .EmailAddress().WithMessage("Geçerli bir eposta bilgisi giriniz.");

        RuleForEach(v => v.IBANs)
            .SetValidator(new IBANCustomerUpdateCommandValidator());

        RuleForEach(v => v.AuthorizePeople)
            .SetValidator(new AuthorizePersonCustomerUpdateCommandValidator());
    }
}

public class IBANCustomerUpdateCommandValidator : AbstractValidator<IBANCustomerUpdateCommand>
{
    public IBANCustomerUpdateCommandValidator()
    {
        RuleFor(v => v.Number)
            .Matches("[A-Z]{2}[0-9]{2}(?:[ ]?[0-9]{4}){5}(?!(?:[ ]?[0-9]){3})(?:[ ]?[0-9]{1,2})?")
            .WithMessage("Geçerli Iban giriniz.");

    }
}

public class AuthorizePersonCustomerUpdateCommandValidator : AbstractValidator<AuthorizePersonCustomerUpdateCommand>
{
    public AuthorizePersonCustomerUpdateCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Yetkili adı boş bırakılamaz.")
            .NotNull().WithMessage("Yetkili adı boş bırakılamaz.");

        RuleFor(v => v.Phone)
            .Matches(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$").WithMessage("Geçerli bir telefon bilgisi giriniz.");

        RuleFor(v => v.EMail)
            .EmailAddress().WithMessage("Geçerli bir eposta bilgisi giriniz.");
    }
}
