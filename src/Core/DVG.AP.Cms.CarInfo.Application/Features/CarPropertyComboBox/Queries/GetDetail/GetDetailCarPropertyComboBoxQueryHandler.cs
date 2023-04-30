using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetDetail;

public class
    GetDetailCarPropertyComboBoxQueryHandler : IRequestHandler<GetDetailCarPropertyComboBoxQuery,
        CarPropertyComboBoxDetail>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public GetDetailCarPropertyComboBoxQueryHandler(
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository, IMapper mapper)
    {
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<CarPropertyComboBoxDetail> Handle(GetDetailCarPropertyComboBoxQuery request,
        CancellationToken cancellationToken)
    {
        var spect = new CarPropertyComboBoxSingleSpec();
        spect.NoTracking();
        spect.GetById(request.Id);
        var carPropertyComboBox = await _carPropertyComboBoxRepository.GetBySpecAsync(spect, cancellationToken);
        NotFoundException.NotFound(carPropertyComboBox, name: nameof(Domain.Entities.CarPropertyComboBox),
            key: request.Id);
        return _mapper.Map<CarPropertyComboBoxDetail>(carPropertyComboBox);
    }
}