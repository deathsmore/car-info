using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetList;

public class GetListCarPropertyComboBoxQueryHandler : IRequestHandler<GetListCarPropertyComboBoxQuery,
    IReadOnlyList<CarPropertyComboBoxGetListVm>>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public GetListCarPropertyComboBoxQueryHandler(
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository, IMapper mapper)
    {
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CarPropertyComboBoxGetListVm>> Handle(GetListCarPropertyComboBoxQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new CarPropertyComboBoxSpecification();
        spec.NoTracking();
        var carPropertyComboBoxs =
            await _carPropertyComboBoxRepository.ListAsync(spec, cancellationToken);
        return _mapper.Map<IReadOnlyList<CarPropertyComboBoxGetListVm>>(carPropertyComboBoxs);
    }
}