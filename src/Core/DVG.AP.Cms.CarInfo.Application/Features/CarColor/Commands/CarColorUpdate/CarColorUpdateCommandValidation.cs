using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorUpdate
{
    public class CarColorUpdateCommandValidation : AbstractValidator<CarColorUpdate>
    {
        public CarColorUpdateCommandValidation()
        {
            RuleFor(o => o.Name)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
               .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);
            RuleFor(o => o.Code)
                .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);
        }
    }
}
