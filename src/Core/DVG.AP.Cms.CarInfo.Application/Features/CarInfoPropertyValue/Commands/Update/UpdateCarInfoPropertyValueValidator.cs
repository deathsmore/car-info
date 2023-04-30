using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;

public class UpdateCarInfoPropertyValueValidator:AbstractValidator<CarInfoPropertyListForUpdate>
{
    public UpdateCarInfoPropertyValueValidator()
    {
        RuleFor(d => d.CarInfoId)
            .NotNull().WithMessage(ValidationMessage.Required)
            .Must(x => long.TryParse(x, out _)).WithMessage(ValidationMessage.MustIsLongType);
        RuleForEach(x => x.CarInfoPropertyValues).SetValidator(new CarInfoPropertyValueUpdateValidator());
    }
}
public class CarInfoPropertyValueUpdateValidator : AbstractValidator<CarInfoPropertyForUpdate>
{
    public CarInfoPropertyValueUpdateValidator()
    {
        // RuleFor(d => d.Value)
        //     .NotEmpty().WithMessage(ValidationMessage.Required)
        //     .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
    }
}