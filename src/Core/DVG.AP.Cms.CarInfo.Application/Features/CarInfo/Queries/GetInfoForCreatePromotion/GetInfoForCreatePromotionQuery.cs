using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetInfoForCreatePromotion
{
    public class GetInfoForCreatePromotionQuery :IRequest<CarInfoForCreatePromotionVm>
    {
        public long CarInfoId { get; set; }
    }

    public class CarInfoForCreatePromotionVm
    {
        public double ListedPrice { get; set; }
        public string Avatar { get; set; }
    }
}
