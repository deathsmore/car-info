using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;

public class FilterCarSpecQueryHandler : IRequestHandler<FilterCarSpectQuery, PagedList<CarSpecFilterVm>>
{
    private readonly ICarInfoPropertyValueRepository _carInfoPropertyValueRepository;
    private readonly  ICarInfoRepository _repository;

    public FilterCarSpecQueryHandler(ICarInfoPropertyValueRepository carInfoRepository,
        ICarInfoRepository repository)
    {
        _carInfoPropertyValueRepository = carInfoRepository;
        _repository = repository;
    }

    public async Task<PagedList<CarSpecFilterVm>> Handle(FilterCarSpectQuery request,
        CancellationToken cancellationToken)
    {
        var paramFilter = request.CarSpectFilterParam;
        return await _repository.GetPagedListCarSpec(paramFilter);
      
    }
}