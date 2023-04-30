using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetDetail;

public class
    GetDetailCarPropertyGroupQueryHandler : IRequestHandler<GetDetailCarPropertyGroupQuery, CarPropertyGroupDetailVm>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _carPropertyGroupRepository;

    public GetDetailCarPropertyGroupQueryHandler(IMapper mapper,
        IRepository<Domain.Entities.CarPropertyGroup> carPropertyGroupRepository)
    {
        _mapper = mapper;
        _carPropertyGroupRepository = carPropertyGroupRepository;
    }

    public async Task<CarPropertyGroupDetailVm> Handle(GetDetailCarPropertyGroupQuery request,
        CancellationToken cancellationToken)
    {
        var carPropertyGroup =
            await _carPropertyGroupRepository.GetBySpecAsync(new CarPropertyGroupDetailByIdSpec(request.Id),
                cancellationToken);
        NotFoundException.NotFound(carPropertyGroup, name: nameof(Domain.Entities.CarPropertyGroup), key: request.Id);
        return _mapper.Map<CarPropertyGroupDetailVm>(carPropertyGroup);
    }
}