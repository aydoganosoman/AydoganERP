using FluentValidation;

namespace AydoganERP.Base.Application.SharedManager.TagManager.Commands.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(v => v.CompanyId).NotEmpty();
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.TagGroupId).NotEmpty();
        RuleFor(v => v.Color).MaximumLength(7).Matches(@"^#[0-9A-Fa-f]{6}$").When(v => !string.IsNullOrEmpty(v.Color));
    }
}