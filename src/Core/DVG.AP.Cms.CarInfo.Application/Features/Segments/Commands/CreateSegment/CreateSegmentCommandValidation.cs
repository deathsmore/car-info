﻿using DVG.AP.Cms.CarInfo.Application.Contracts.Constant;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.CreateSegment
{
    public class CreateSegmentCommandValidation : AbstractValidator<SegmentForCreation>
    {
        public CreateSegmentCommandValidation()
        {
            RuleFor(o => o.Name)
               .NotNull().NotEmpty().WithMessage(ValidationMessage.Required)
               .MaximumLength(255).WithMessage(ValidationMessage.Maxlength);
        }
    }
}
