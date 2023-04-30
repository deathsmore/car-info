using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Create;

public class CarPropertyComboBoxCreationValidator : AbstractValidator<CarPropertyComboBoxForCreation>
{
    public CarPropertyComboBoxCreationValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(200).WithMessage(ValidationMessage.Maxlength);
        RuleForEach(x => x.CarPropertyComboboxOptions)
            .SetValidator(new CarPropertyComboBoxOptionForCreationValidator());
    }
}

public class CarPropertyComboBoxOptionForCreationValidator : AbstractValidator<CarPropertyComboBoxOptionForCreation>
{
    public CarPropertyComboBoxOptionForCreationValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage(ValidationMessage.Required)
            .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);
    }
}