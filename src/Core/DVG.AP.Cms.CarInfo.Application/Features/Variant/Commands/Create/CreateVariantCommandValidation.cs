using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Create
{
    public class CreateVariantCommandValidation : AbstractValidator<VariantForCreation>
    {
        public CreateVariantCommandValidation()
        {
            RuleFor(o => o.Name)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
               .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.ModelId)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
