using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Create;

public class CarPropertyGroupCreationValidator : AbstractValidator<CarPropertyGroupForCreation>
{
    public CarPropertyGroupCreationValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
        RuleForEach(x => x.CarProperties).SetValidator(new CarPropertyCreationValidator());
    }
}

public class CarPropertyCreationValidator : AbstractValidator<CarPropertyForCreation>
{
    public CarPropertyCreationValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
    }
}