using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

using MediatR;


namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.Filter
{
    public class FilterVariantQueryHandler : IRequestHandler<FilterVariantQuery, PagedList<FilterVariantVm>>
    {
        private readonly IVariantRepository _variantRepository;
        public FilterVariantQueryHandler(IVariantRepository variantRepository)
        {
            _variantRepository = variantRepository;
        }
        public async Task<PagedList<FilterVariantVm>> Handle(FilterVariantQuery request, CancellationToken cancellationToken)
        {
            return await _variantRepository.FilterAsync(request.FilterVariantParameter);
        }
    }
}
