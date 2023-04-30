using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllForCRUDPromotion
{
    public class GetAllModelForCRUDPromotionQuery : IRequest<IReadOnlyList<ModelVm>>
    {
        public int BrandId { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
