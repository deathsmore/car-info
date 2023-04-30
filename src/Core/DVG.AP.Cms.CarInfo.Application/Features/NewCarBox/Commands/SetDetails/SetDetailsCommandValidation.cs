using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Commands.SetDetails
{
    public class SetDetailsCommandValidation : AbstractValidator<NewCarBoxSetDetail>
    {
        public SetDetailsCommandValidation()
        {
            RuleFor(p => p.NewCarBoxId)
                .GreaterThan(0).WithMessage(ValidationMessage.GreaterThan)
                .Must(p => !p.Equals(default)).WithMessage(ValidationMessage.IsNotValid);

            RuleForEach(p => p.Items).SetValidator(new NewCarBoxDetailItemValidation());
        }
        public SetDetailsCommandValidation(int numberDisplay)
        {
            RuleFor(p => p.Items).NotNull().WithMessage(ValidationMessage.Required)
                .Must(x => x.Count >= numberDisplay).WithMessage(ValidationMessage.AtleastMinItems);

            RuleFor(p => p.Items).Must(p => p.GroupBy(x => x.NewCarArticleId).Count() == p.Count())
                .WithMessage(ValidationMessage.Duplicate);
        }
        
    }

    public class NewCarBoxDetailItemValidation : AbstractValidator<NewCarBoxDetailItem>
    {
        public NewCarBoxDetailItemValidation()
        {
            RuleFor(b => b.ObjectId)
                .NotEmpty().WithMessage(ValidationMessage.Required);

            RuleFor(b => b.Ordinal)
                .GreaterThanOrEqualTo(short.Parse("0")).WithMessage(ValidationMessage.Required);
        }
    }
}
