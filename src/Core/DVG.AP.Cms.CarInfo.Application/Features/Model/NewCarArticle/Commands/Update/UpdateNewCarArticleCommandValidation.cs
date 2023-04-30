using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models;
using FluentValidation;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update
{
    public class UpdateNewCarArticleCommandValidation : AbstractValidator<NewCarArticleForUpdate>
    {
        public UpdateNewCarArticleCommandValidation()
        {
            RuleFor(o => o.Title)
                .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(256).WithMessage(ValidationMessage.Maxlength);
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

            RuleForEach(o => o.Contents).SetValidator(new NewCarContentUpdateValidation());
            RuleForEach(o => o.Images).SetValidator(new NewCarImageUpdateValidation());
        }
    }

    public class NewCarContentUpdateValidation : AbstractValidator<ContentForUpdate>
    {
        public NewCarContentUpdateValidation()
        {
            RuleFor(o => o.ContentType)
                .NotNull().WithMessage(ValidationMessage.Required);
        }
    }

    public class NewCarImageUpdateValidation : AbstractValidator<ImageForUpdate>
    {
        public NewCarImageUpdateValidation()
        {
            RuleFor(i => i.AltText)
                .NotEmpty().WithMessage(ValidationMessage.Required)
                .MaximumLength(250).WithMessage(ValidationMessage.Maxlength);

            RuleFor(i => i.Type)
                .NotNull().WithMessage(ValidationMessage.Required);
        }
    }
}
