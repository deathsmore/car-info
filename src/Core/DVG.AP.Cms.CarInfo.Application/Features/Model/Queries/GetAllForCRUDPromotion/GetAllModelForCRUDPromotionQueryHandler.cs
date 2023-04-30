using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
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
    public class GetAllModelForCRUDPromotionQueryHandler : IRequestHandler<GetAllModelForCRUDPromotionQuery, IReadOnlyList<ModelVm>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        public GetAllModelForCRUDPromotionQueryHandler(
            IModelRepository modelRepository,
            IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<ModelVm>> Handle(GetAllModelForCRUDPromotionQuery request, CancellationToken cancellationToken)
        {
            if (WebsiteManager.SiteName == SiteName.Oto.ToString())
            {
                return _mapper.Map<IReadOnlyList<ModelVm>>(await _modelRepository.GetListHaveActiveNewCarAsync(request.BrandId, request.Status));
            }    
            else
            {
                return _mapper.Map<IReadOnlyList<ModelVm>>(await _modelRepository.GetListAsync(request.BrandId, request.Status));
            }
        }
    }
}
