using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail
{
    public class GetVariantDetailQueryHandler : IRequestHandler<GetVariantDetailQuery, VariantDetailVm>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;
        public GetVariantDetailQueryHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }
        public async Task<VariantDetailVm> Handle(GetVariantDetailQuery request, CancellationToken cancellationToken)
        {
            var variant = await _variantRepository.GetDetail(request.Id);
            NotFoundException.NotFound(variant, name: nameof(Variant), key: request.Id);
            return variant;
        }
    }
}
