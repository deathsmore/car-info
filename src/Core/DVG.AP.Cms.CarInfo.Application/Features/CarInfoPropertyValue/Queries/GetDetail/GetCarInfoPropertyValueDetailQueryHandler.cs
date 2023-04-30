using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.GetDetail;

public class
    GetCarInfoPropertyValueDetailQueryHandler : IRequestHandler<GetCarInfoPropertyValueDetailQuery,
        CarInfoDetailIncludePropertyValueVm>
{
    private readonly ICarInfoRepository _carInfoRepository;
    private readonly ICarInfoPropertyValueRepository _carInfoPropertyValueRepository;
    private readonly IMapper _mapper;

    public GetCarInfoPropertyValueDetailQueryHandler(ICarInfoRepository carInfoRepository,
        ICarInfoPropertyValueRepository carInfoPropertyValueRepository, IMapper mapper)
    {
        _carInfoRepository = carInfoRepository;
        _carInfoPropertyValueRepository = carInfoPropertyValueRepository;
        _mapper = mapper;
    }

    public async Task<CarInfoDetailIncludePropertyValueVm> Handle(GetCarInfoPropertyValueDetailQuery request,
        CancellationToken cancellationToken)
    {
        var carInfo =
            await _carInfoRepository.GetDetailIncludePropertyValue(request.CarInfoId);
        if (carInfo is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.CarInfo), new { request.CarInfoId });
        }


        var carInfoPropertyValueDetailVm = new CarInfoDetailIncludePropertyValueVm()
        {
            CarInfoId = carInfo.Id.ToString(),
            BrandId = carInfo.Variant.Model.BrandId,
            ModelId = carInfo.Variant.ModelId,
            Year = carInfo.Year,
            VariantId = carInfo.VariantId,
            CarInfoPropertyValueDetailVms =
                _mapper.Map<IEnumerable<CarInfoPropertyValueDetailVm>>(carInfo.CarInfoPropertyValues)
        };
        return carInfoPropertyValueDetailVm;
    }
}