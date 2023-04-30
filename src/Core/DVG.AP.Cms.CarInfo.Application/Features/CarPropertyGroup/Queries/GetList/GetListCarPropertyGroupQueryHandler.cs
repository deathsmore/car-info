using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetList;

public class
    GetListCarPropertyGroupQueryHandler : IRequestHandler<GetListCarGroupQuery,
        IReadOnlyList<CarPropertyGroupGetListVm>>
{
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _carPropertyGroupRepository;
    private readonly IMapper _mapper;

    public GetListCarPropertyGroupQueryHandler(IRepository<Domain.Entities.CarPropertyGroup> carPropertyGroupRepository,
        IMapper mapper)
    {
        _carPropertyGroupRepository = carPropertyGroupRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CarPropertyGroupGetListVm>> Handle(GetListCarGroupQuery request,
        CancellationToken cancellationToken)
    {
        var carPropertyGroupSpec = new CarPropertyGroupSpecification();
        carPropertyGroupSpec.IncludeChild();

        var carPropertyGroups =
            await _carPropertyGroupRepository.ListAsync(carPropertyGroupSpec, cancellationToken);
        var result = _mapper.Map<IReadOnlyList<CarPropertyGroupGetListVm>>(carPropertyGroups);
        return result;
    }
}