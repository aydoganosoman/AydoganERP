using FluentValidation;

namespace AydoganERP.Base.Application.SharedManager.TagGroupManager.Commands.Create;

public class CreateTagGroupCommandValidator : AbstractValidator<CreateTagGroupCommand>
{
    public CreateTagGroupCommandValidator()
    {
        RuleFor(v => v.CompanyId).NotEmpty();
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
    }
}