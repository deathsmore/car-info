using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Create;

public class CreateCarPropertyGroupCommandHandler : IRequestHandler<CreateCarPropertyGroupCommand, int>
{
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _carPropertyGroupRepository;
    private readonly IMapper _mapper;

    public CreateCarPropertyGroupCommandHandler(
        IRepository<Domain.Entities.CarPropertyGroup> carPropertyGroupRepository, IMapper mapper)
    {
        _carPropertyGroupRepository = carPropertyGroupRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCarPropertyGroupCommand request, CancellationToken cancellationToken)
    {
        await Guard.Against.Validate(request.CarPropertyGroupForCreation, new CarPropertyGroupCreationValidator());
        var carPropertyGroup = _mapper.Map<Domain.Entities.CarPropertyGroup>(request.CarPropertyGroupForCreation);
        await _carPropertyGroupRepository.AddAsync(carPropertyGroup, cancellationToken);
        return carPropertyGroup.Id;
    }
}