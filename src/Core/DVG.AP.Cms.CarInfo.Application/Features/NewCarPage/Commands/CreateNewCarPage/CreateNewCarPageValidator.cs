using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.CreateNewCarPage
{
    public class CreateNewCarPageValidator : AbstractValidator<NewCarPageForCreation>
    {
        public CreateNewCarPageValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage(ValidationMessage.Required);
            RuleFor(p => p.Type)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(p => p.Status)
                .NotNull().WithMessage(ValidationMessage.Required);
        }
    }
}
