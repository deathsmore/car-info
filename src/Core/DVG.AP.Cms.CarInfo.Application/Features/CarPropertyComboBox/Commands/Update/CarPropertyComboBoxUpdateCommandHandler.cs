using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Update;

public class CarPropertyComboBoxUpdateCommandHandler : IRequestHandler<CarPropertyComboBoxUpdateCommand>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public CarPropertyComboBoxUpdateCommandHandler(
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository, IMapper mapper)
    {
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CarPropertyComboBoxUpdateCommand request, CancellationToken cancellationToken)
    {
        await Guard.Against.Validate(request.ComboBoxForUpdate, new CarPropertyComboBoxUpdateValidator());
        var carPropertyComboBoxSpec = new CarPropertyComboBoxSingleSpec();
        carPropertyComboBoxSpec.GetById(request.Id);
        var carPropertyComboBox =
            await _carPropertyComboBoxRepository.GetBySpecAsync(carPropertyComboBoxSpec, cancellationToken);
        NotFoundException.NotFound(carPropertyComboBox, name: nameof(Domain.Entities.CarPropertyComboBox),
            key: request.Id);

        _mapper.Map(request.ComboBoxForUpdate, carPropertyComboBox, typeof(CarPropertyComboBoxForUpdate),
            typeof(Domain.Entities.CarPropertyComboBox));
        await _carPropertyComboBoxRepository.UpdateAsync(carPropertyComboBox, cancellationToken);
        return Unit.Value;
    }
}