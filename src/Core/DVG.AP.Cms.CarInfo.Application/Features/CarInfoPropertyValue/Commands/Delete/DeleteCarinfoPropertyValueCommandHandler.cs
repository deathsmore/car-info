using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Delete;

public class DeleteCarInfoPropertyValueCommandHandler : IRequestHandler<DeleteCarInfoPropertyValueCommand>
{
    private readonly ICarInfoPropertyValueRepository _carInfoPropertyValueRepository;

    public DeleteCarInfoPropertyValueCommandHandler(ICarInfoPropertyValueRepository carInfoPropertyValueRepository)
    {
        _carInfoPropertyValueRepository = carInfoPropertyValueRepository;
    }

    public async Task<Unit> Handle(DeleteCarInfoPropertyValueCommand request, CancellationToken cancellationToken)
    {
        var carInfoProValues = await _carInfoPropertyValueRepository
            .ListAsync(request.CarInfoId);
        if (!carInfoProValues.Any())
        {
            throw new NotFoundException(nameof(Domain.Entities.CarInfoPropertyValue),
                new { request.CarInfoId });
        }

// delete all car Spec
        await _carInfoPropertyValueRepository.DeleteRangeAsync(carInfoProValues, cancellationToken);
        return Unit.Value;
    }
}