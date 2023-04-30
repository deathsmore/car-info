using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Delete;

public class DelteCarPropertyComboBoxCommandHandler : IRequestHandler<DelteCarPropertyComboBoxCommand>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _repository;

    public DelteCarPropertyComboBoxCommandHandler(IRepository<Domain.Entities.CarPropertyComboBox> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DelteCarPropertyComboBoxCommand request, CancellationToken cancellationToken)
    {
        var carPropertyComboBoxSpec = new CarPropertyComboBoxSingleSpec();

        carPropertyComboBoxSpec.GetById(request.Id);
        var carPropertyComboBox =
            await _repository.GetBySpecAsync(carPropertyComboBoxSpec, cancellationToken);
        NotFoundException.NotFound(carPropertyComboBox, name: nameof(Domain.Entities.CarPropertyComboBox),
            key: request.Id);
        await _repository.DeleteAsync(carPropertyComboBox, cancellationToken);
        return Unit.Value;
    }
}