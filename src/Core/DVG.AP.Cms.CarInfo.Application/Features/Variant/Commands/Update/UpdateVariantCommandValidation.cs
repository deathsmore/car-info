using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Update
{
    public class UpdateVariantCommandValidation : AbstractValidator<VariantForUpdate>
    {
        public UpdateVariantCommandValidation()
        {
            //RuleFor(o => o.Id)
            //   .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.Name)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
               .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.ModelId)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required);
        }
    }
}
