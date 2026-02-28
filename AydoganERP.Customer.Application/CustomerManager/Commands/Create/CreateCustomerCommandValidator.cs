using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using FluentValidation;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(v => v.CompanyId)
            .NotEmpty().WithMessage("CompanyId is required.");

        RuleFor(v => v.Code)
            .NotEmpty().WithMessage("Code is required.");

        RuleFor(v => v.CustomerName)
            .NotEmpty().WithMessage("CustomerName is required.");

        RuleFor(v => v.Type)
            .Must(type => type is CustomerTypeEnum.Customer or CustomerTypeEnum.Suplier or CustomerTypeEnum.Both)
            .WithMessage("Customer type is not valid.");
    }
}
