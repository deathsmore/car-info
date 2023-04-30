using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Delete;

public class DeleteCarPropertyGroupCommandHandler: IRequestHandler<DeleteCarPropertyGroupCommand>
{
    
    private readonly IRepository<Domain.Entities.CarPropertyGroup> _repository;

    public DeleteCarPropertyGroupCommandHandler(IRepository<Domain.Entities.CarPropertyGroup> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteCarPropertyGroupCommand request, CancellationToken cancellationToken)
    {
        var carPropertyGroup = await _repository.GetBySpecAsync(
            new CarPropertyGroupDetailByIdSpec(request.Id), cancellationToken);
        if (carPropertyGroup is null)
        {
            throw new NotFoundException(nameof(Domain.Entities.CarPropertyGroup), request.Id);
        }

        await _repository.DeleteAsync(carPropertyGroup, cancellationToken);
        return Unit.Value;
    }
}