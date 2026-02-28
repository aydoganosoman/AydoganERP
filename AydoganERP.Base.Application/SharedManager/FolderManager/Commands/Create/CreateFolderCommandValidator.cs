using FluentValidation;

namespace AydoganERP.Base.Application.SharedManager.FolderManager.Commands.Create;

public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
{
    public CreateFolderCommandValidator()
    {
        RuleFor(v => v.CompanyId).NotEmpty();
        RuleFor(v => v.Code).NotEmpty().MaximumLength(50);
        RuleFor(v => v.Name).NotEmpty().MaximumLength(200);
        RuleFor(v => v.Color).MaximumLength(7).Matches(@"^#[0-9A-Fa-f]{6}$").When(v => !string.IsNullOrEmpty(v.Color));
    }
}