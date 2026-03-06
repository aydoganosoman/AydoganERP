using FluentValidation;

namespace AydoganERP.Inventory.Application.ProductManager.Commands.Create;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        // UnitId opsiyonel - boşsa handler varsayılan birim atar

        RuleFor(v => v.PurchaseVatRate)
            .InclusiveBetween(0, 100).WithMessage("PurchaseVatRate must be between 0 and 100.");
    }
}
