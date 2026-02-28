using FluentValidation;

namespace AydoganERP.Base.Application.SharedManager.GroupManager.Commands.Create;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(v => v.CompanyId).NotEmpty();
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
    }
}