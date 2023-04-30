using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.Variant;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll
{
    public class GetAllVariantQueryHandler : IRequestHandler<GetAllVariantQuery, IReadOnlyList<VariantVm>>
    {
        private IRepository<Domain.Entities.Variant> _variantRepository;
        public GetAllVariantQueryHandler(IRepository<Domain.Entities.Variant> variantRepository)
        {
            _variantRepository = variantRepository;
        }
        public async Task<IReadOnlyList<VariantVm>> Handle(GetAllVariantQuery request, CancellationToken cancellationToken)
        {
            var variantSpec = new VariantSpec();
            variantSpec.Select();
            variantSpec.GetByModel(request.ModelId);
            var variants = await _variantRepository.ListAsync(variantSpec);
            return variants.Select(m => new VariantVm()
            {
                Id = m.Id,
                ModelId = m.ModelId,
                Name = m.Name,
                IsVirtual = m.IsVirtual
            }).ToList();
        }
    }
}
