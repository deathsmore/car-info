using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos
{
    public class GetCarInfoByModelAndStatusQueryHandler : IRequestHandler<GetCarInfoByModelAndStatusQuery,
            IReadOnlyList<CarInfoVm>>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IMapper _mapper;

        public GetCarInfoByModelAndStatusQueryHandler(ICarInfoRepository carInfoRepository, IMapper mapper)
        {
            _carInfoRepository = carInfoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CarInfoVm>> Handle(GetCarInfoByModelAndStatusQuery request,
            CancellationToken cancellationToken)
        {
            var lstCarInfo = await _carInfoRepository.ListAsync(new GetCarInfosSpec(request.VariantId, request.Status), cancellationToken);

            //if (!lstCarInfo.Any())
            //{
            //    throw new NotFoundException(nameof(Domain.Entities.CarInfo),
            //        new { request.ModelId, request.Status });
            //}
            var result = _mapper.Map<IReadOnlyList<CarInfoVm>>(lstCarInfo);

            return result;
        }
    }
}
