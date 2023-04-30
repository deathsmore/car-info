using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.Filter;

public class
    FilterCarPropertyGroupQueryHandler : IRequestHandler<FilterCarPropertyGroupQuery,
        IReadOnlyList<CarPropertyGroupFilterVm>>
{
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _repository;
    private readonly IMapper _mapper;

    public FilterCarPropertyGroupQueryHandler(IRepository<Domain.Entities.CarPropertyGroup> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CarPropertyGroupFilterVm>> Handle(FilterCarPropertyGroupQuery request,
        CancellationToken cancellationToken)
    {
        var carPropertyGroupSpec = new CarPropertyGroupSpecification();
        carPropertyGroupSpec.Filter(request.CarPropertyGroupFilterParam.KeywordSearch,request.CarPropertyGroupFilterParam.Status);
        
        var carPropertyGroups = await _repository.ListAsync(carPropertyGroupSpec, cancellationToken);
        var result = _mapper.Map<IReadOnlyList<CarPropertyGroupFilterVm>>(carPropertyGroups);
        return result;
    }
}