using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate.Models;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate
{
    class CarInfoUpdateCommandValidation : AbstractValidator<CarInfoUpdate>
    {
        public CarInfoUpdateCommandValidation()
        {
            RuleFor(o => o.Id)
            .GreaterThan(0).WithMessage(ValidationMessage.GreaterThan);

            RuleFor(o => o.Name)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
               .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.Avatar)
                .MaximumLength(500).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.Url)
                .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.Engine)
                .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);

            // RuleForEach(o => o.Prices).SetValidator(new CarPriceValidation());
            RuleForEach(o => o.Images).SetValidator(new CarImageValidation());
        }
    }

    public class CarPriceValidation : AbstractValidator<CarPriceForUpdate>
    {
        public CarPriceValidation()
        {
            RuleFor(o => o.CarInfoId)
                .GreaterThan(0).WithMessage(ValidationMessage.GreaterThan);
        }
    }

    public class CarImageValidation : AbstractValidator<CarImageForUpdate>
    {
        public CarImageValidation()
        {

          
            RuleFor(i => i.AltText)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(250).WithMessage(ValidationMessage.Maxlength);
            RuleFor(i => i.Type)
                .NotNull().WithMessage(ValidationMessage.Required);
            RuleFor(o => o.Title)
                .MaximumLength(500).WithMessage(ValidationMessage.Maxlength);
            RuleFor(o => o.ColorCode)
                .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);
        }
    }
}
