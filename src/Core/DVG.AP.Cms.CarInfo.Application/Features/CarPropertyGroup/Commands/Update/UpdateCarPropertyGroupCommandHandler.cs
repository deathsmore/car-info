using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Update;

public class UpdateCarPropertyGroupCommandHandler : IRequestHandler<UpdateCarPropertyGroupCommand>
{
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _carPropertyGroupRepository;
    private readonly IMapper _mapper;

    public UpdateCarPropertyGroupCommandHandler(
        IRepository<Domain.Entities.CarPropertyGroup> carPropertyGroupRepository, IMapper mapper)
    {
        _carPropertyGroupRepository = carPropertyGroupRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCarPropertyGroupCommand request, CancellationToken cancellationToken)
    {
        var carPropertyGroup = await _carPropertyGroupRepository.GetBySpecAsync(
            new CarPropertyGroupDetailByIdSpec(request.CarPropertyGroupForUpdate.Id), cancellationToken);
        if (carPropertyGroup is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.CarPropertyGroup), request.CarPropertyGroupForUpdate.Id);
        }

        _mapper.Map(request.CarPropertyGroupForUpdate, carPropertyGroup, typeof(CarPropertyForUpdate),
            typeof(Domain.Entities.CarPropertyGroup));
        await _carPropertyGroupRepository.UpdateAsync(carPropertyGroup, cancellationToken);
        return Unit.Value;
    }
}