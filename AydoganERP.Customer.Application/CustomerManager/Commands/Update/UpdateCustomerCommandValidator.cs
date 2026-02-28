using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using FluentValidation;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(v => v.CustomerName)
            .NotEmpty().WithMessage("CustomerName is required.");

        RuleFor(v => v.Type)
            .Must(type => type is CustomerTypeEnum.Customer or CustomerTypeEnum.Suplier or CustomerTypeEnum.Both)
            .WithMessage("Customer type is not valid.");
    }
}
