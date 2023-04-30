using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAllForCRUDPromotion
{
    public class GetAllVariantForCRUDPromotionQuery : IRequest<IReadOnlyList<VariantVm>>
    {
        public int ModelId { get; set; }
    }
}
