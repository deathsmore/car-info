using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;

public class CreateCarInfoPropertyValueValidator : AbstractValidator<CarInfoPropertyListForCreation>
{
    public CreateCarInfoPropertyValueValidator()
    {
        RuleFor(d => d.CarInfoId)
            .NotNull().WithMessage(ValidationMessage.Required)
            .Must(x => long.TryParse(x, out _)).WithMessage(ValidationMessage.MustIsLongType);
        RuleForEach(x => x.CarInfoPropertyValues).SetValidator(new CarInfoPropertyValueCreationValidator());
    }
}
public class CarInfoPropertyValueCreationValidator : AbstractValidator<CarInfoPropertyForCreation>
{
    public CarInfoPropertyValueCreationValidator()
    {
        // RuleFor(d => d.Value)
        //     .NotEmpty().WithMessage(ValidationMessage.Required)
        //     .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
    }
}