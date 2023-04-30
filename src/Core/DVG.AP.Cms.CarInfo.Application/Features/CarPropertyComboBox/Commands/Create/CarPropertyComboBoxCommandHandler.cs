using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Create;

public class CarPropertyComboBoxCommandHandler : IRequestHandler<CarPropertyComboBoxCreateCommand, int>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public CarPropertyComboBoxCommandHandler(
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository, IMapper mapper)
    {
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CarPropertyComboBoxCreateCommand request, CancellationToken cancellationToken)
    {
        await Guard.Against.Validate(request.ComboBoxForCreation, new CarPropertyComboBoxCreationValidator());
        var carPropertyComboBox = _mapper.Map<Domain.Entities.CarPropertyComboBox>(request.ComboBoxForCreation);
        await _carPropertyComboBoxRepository.AddAsync(carPropertyComboBox, cancellationToken);
        return carPropertyComboBox.Id;
    }
}