using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarImage;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.District;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail
{
    public class GetCarInfoDetailQueryHandler : IRequestHandler<GetCarInfoDetailQuery, CarInfoGetDetailVm>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IRepository<Domain.Entities.CarImage> _carImageRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IRepository<Domain.Entities.Model> _modelRepository;
        private readonly IRepository<Domain.Entities.Variant> _variantRepository;
        private readonly IMapper _mapper;
        private readonly ICommonRepository<Url> _urlRepository;
        private readonly ICommonRepository<District> _districtRepository;

        public GetCarInfoDetailQueryHandler(
            ICarInfoRepository carInfoRepository,
            IRepository<Domain.Entities.CarImage> carImageRepository,
            IBrandRepository brandRepository,
            IRepository<Domain.Entities.Model> modelRepository,
            IRepository<Domain.Entities.Variant> variantRepository,
            IMapper mapper,
            ICommonRepository<Url> urlRepository,
            ICommonRepository<District> districtRepository)
        {
            _carInfoRepository = carInfoRepository;
            _carImageRepository = carImageRepository;
            _brandRepository = brandRepository;
            _modelRepository = modelRepository;
            _variantRepository = variantRepository;
            _mapper = mapper;
            _urlRepository = urlRepository;
            _districtRepository = districtRepository;
        }

        public async Task<CarInfoGetDetailVm> Handle(GetCarInfoDetailQuery request, CancellationToken cancellationToken)
        {
            var carInfo = await _carInfoRepository.GetBySpecAsync(new CarInfoDetailSpec(request.Id), cancellationToken);
            NotFoundException.NotFound(carInfo, name: nameof(CarInfo), key: request.Id);
            var result = _mapper.Map<CarInfoGetDetailVm>(carInfo);

            var pricesByLocation = carInfo.Prices.Where(x=> x.OptionType == CarPriceOptionType.ByLocation).ToList();

            if (pricesByLocation.Any())
            {
                var districtSpec = new DistrictSpec();
                districtSpec.GetByIds(pricesByLocation.Select(p => p.OptionId).ToList());
                districtSpec.IncludeRelations();
                districtSpec.Select();

                var listDistrictInfo = await _districtRepository.ListAsync(districtSpec, cancellationToken);
                result.PricesByLocation = pricesByLocation.Join(listDistrictInfo,
                    p => p.OptionId,
                    d => d.Id,
                    (p, d) => new CarPriceByLocationVm(p, d)).ToList();
            }
            
            var urlCarInfoSpec = new UrlCarInfoSpec();
            urlCarInfoSpec.GetByCarInfo(carInfo.Id);
            var url = await _urlRepository.GetBySpecAsync(urlCarInfoSpec,cancellationToken);
            result.Url = url != null ? url.Slug : null;

            return result;
        }
    }
}