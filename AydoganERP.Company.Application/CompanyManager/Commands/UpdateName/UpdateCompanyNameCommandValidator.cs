using FluentValidation;

namespace AydoganERP.Company.Application.CompanyManager.Commands.UpdateName;

public class UpdateCompanyNameCommandValidator : AbstractValidator<UpdateCompanyNameCommand>
{
    public UpdateCompanyNameCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}
