using AydoganERP.Base.Domain.Modules.CustomerModule.Enums;
using FluentValidation;

namespace AydoganERP.Customer.Application.CustomerManager.Commands.ChangeStatus;

public class ChangeCustomerStatusCommandValidator : AbstractValidator<ChangeCustomerStatusCommand>
{
    public ChangeCustomerStatusCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(v => v.Status)
            .Must(status => status is CustomerStatusEnum.Active or CustomerStatusEnum.Passive or CustomerStatusEnum.Blocked)
            .WithMessage("Customer status is not valid.");
    }
}
