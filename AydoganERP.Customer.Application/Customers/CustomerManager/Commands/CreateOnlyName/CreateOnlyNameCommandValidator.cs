using FluentValidation;

namespace AydoganERP.Customer.Application.Customers.CustomerManager.Commands.CreateOnlyName;

public class CreateOnlyNameCommandValidator : AbstractValidator<CreateOnlyNameCommand>
{
    public CreateOnlyNameCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Müşteri adı boş bırakılamaz.")
            .NotNull().WithMessage("Müşteri adı boş bırakılamaz.");

    }
}
