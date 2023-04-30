using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create.Models;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create
{
    public class CreateNewCarArticleCommandValidation : AbstractValidator<NewCarArticleForCreation>
    {
        public CreateNewCarArticleCommandValidation()
        {
            RuleFor(o => o.Title)
                .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);

            RuleFor(o => o.Type)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.BrandId)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.ModelId)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.Status)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.NewCarSEOInfos.ContentAngleTag)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.NewCarSEOInfos.ContentFormatTag)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleFor(o => o.NewCarSEOInfos.SourceTag)
                .NotNull().WithMessage(ValidationMessage.Required);

            RuleForEach(o => o.Contents).SetValidator(new NewCarContentValidation());
            RuleForEach(o => o.Images).SetValidator(new NewCarImageValidation());
        }

        public class NewCarContentValidation : AbstractValidator<ContentForCreation>
        {
            public NewCarContentValidation()
            {
                RuleFor(o => o.ContentType)
                    .NotNull().WithMessage(ValidationMessage.Required);
            }
        }

        public class NewCarImageValidation : AbstractValidator<ImageForCreation>
        {
            public NewCarImageValidation()
            {
                RuleFor(i => i.AltText)
                    .NotEmpty().WithMessage(ValidationMessage.Required)
                    .MaximumLength(250).WithMessage(ValidationMessage.Maxlength);

                RuleFor(i => i.Type)
                    .NotNull().WithMessage(ValidationMessage.Required);
            }
        }
    }
}
