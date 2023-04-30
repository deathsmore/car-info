using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Application.Features.Brand.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IReadOnlyList<BrandVm>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public GetAllQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandVm>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var brandSpec = new BrandSpec();
            
            brandSpec.GetByStatus(request.Status);
            brandSpec.Select();

            var brands = await _brandRepository.ListAsync(brandSpec);
            return brands.Select(b => new BrandVm()
            {
                Id = b.Id,
                Name = b.Name,
                Status = b.Status
            }).ToList();
        }
    }
}
