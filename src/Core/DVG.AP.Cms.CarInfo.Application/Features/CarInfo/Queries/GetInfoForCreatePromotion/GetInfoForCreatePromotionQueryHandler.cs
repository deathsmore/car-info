using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetInfoForCreatePromotion
{
    public class GetInfoForCreatePromotionQueryHandler : IRequestHandler<GetInfoForCreatePromotionQuery, CarInfoForCreatePromotionVm>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        public GetInfoForCreatePromotionQueryHandler(ICarInfoRepository carInfoRepository)
        {
            _carInfoRepository = carInfoRepository;
        }
        public async Task<CarInfoForCreatePromotionVm> Handle(GetInfoForCreatePromotionQuery request, CancellationToken cancellationToken)
        {
            var carInfo = await _carInfoRepository.GetInfoForCreatePromotion(request.CarInfoId);
            NotFoundException.NotFound(carInfo, name: nameof(CarInfo), key: request.CarInfoId);
            return carInfo;
        }
    }
}
