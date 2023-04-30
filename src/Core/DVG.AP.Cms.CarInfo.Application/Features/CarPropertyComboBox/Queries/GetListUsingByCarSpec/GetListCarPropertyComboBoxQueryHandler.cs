using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetListUsingByCarSpec;

public class GetListCarPropertyComboBoxUsedQueryHandler : IRequestHandler<GetListCarPropertyComboBoxUsedQuery,
    IReadOnlyList<CarPropertyComboBoxGetListUsedVm>>
{
    private readonly IRepository<CarProperty> _carPropertyRepository;
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public GetListCarPropertyComboBoxUsedQueryHandler(IRepository<CarProperty> carPropertyRepository,
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository,
        IMapper mapper)
    {
        _carPropertyRepository = carPropertyRepository;
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CarPropertyComboBoxGetListUsedVm>> Handle(GetListCarPropertyComboBoxUsedQuery request,
        CancellationToken cancellationToken)
    {
        // Get ComboBox used
        var comboBoxIds = await _carPropertyRepository.ListAsync(new CarPropertySpecification());
        if (!comboBoxIds.Any()) throw new NotFoundException("Notfound CarPropertyComboBox", comboBoxIds);
        
        var comboBoxs =
            await _carPropertyComboBoxRepository.ListAsync(new CarPropertyComboBoxSpecification(comboBoxIds));
        var result = _mapper.Map<IReadOnlyList<CarPropertyComboBoxGetListUsedVm>>(comboBoxs);
        return result;

    }
}