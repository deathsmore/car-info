using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Update;

public class CarPropertyComboBoxUpdateValidator: AbstractValidator<CarPropertyComboBoxForUpdate>
{
    public CarPropertyComboBoxUpdateValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
        RuleForEach(x => x.CarPropertyComboboxOptions)
            .SetValidator(new CarPropertyComboBoxOptionForUpdateValidator());
    }
}

public class CarPropertyComboBoxOptionForUpdateValidator : AbstractValidator<CarPropertyComboBoxOptionForUpdate>
{
    public CarPropertyComboBoxOptionForUpdateValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(256).WithMessage(ValidationMessage.Maxlength); 
    }
}