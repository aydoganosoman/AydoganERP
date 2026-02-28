using AydoganERP.Base.Domain.Modules.InventoryModule.Enums;
using FluentValidation;

namespace AydoganERP.Inventory.Application.StockMovementManager.Commands.Create;

public class CreateStockMovementCommandValidator : AbstractValidator<CreateStockMovementCommand>
{
    public CreateStockMovementCommandValidator()
    {
        RuleFor(v => v.ProductId)
            .NotEmpty().WithMessage("ProductId is required.");

        RuleFor(v => v.Type)
            .Must(type => type is StockMovementTypeEnum.Opening
                or StockMovementTypeEnum.PurchaseIn
                or StockMovementTypeEnum.SaleOut
                or StockMovementTypeEnum.Adjustment)
            .WithMessage("Invalid stock movement type.");

        RuleFor(v => v.QuantityDelta)
            .NotEqual(0).WithMessage("QuantityDelta cannot be 0.");
    }
}
