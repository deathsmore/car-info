using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAllForCRUDPromotion
{
    public class GetAllVariantForCRUDPromotionQueryHandler : IRequestHandler<GetAllVariantForCRUDPromotionQuery, IReadOnlyList<VariantVm>>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;
        public GetAllVariantForCRUDPromotionQueryHandler(
            IVariantRepository variantRepository,
            IMapper mapper
        )
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<VariantVm>> Handle(GetAllVariantForCRUDPromotionQuery request, CancellationToken cancellationToken)
        {
            if (WebsiteManager.SiteName == SiteName.Oto.ToString())
            {
                return _mapper.Map<IReadOnlyList<VariantVm>>(await _variantRepository.GetListHaveActiveNewCarAsync(request.ModelId));
            }
            else
            {
                return _mapper.Map<IReadOnlyList<VariantVm>>(await _variantRepository.GetListAsync(request.ModelId));
            }
        }
    }
}
