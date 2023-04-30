using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;
using DVG.AutoPortal.Core.Infrastructures.Utilies;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Filter;

public class FilterCarPropertyComboBoxQueryHandler : IRequestHandler<FilterCarPropertyComboBoxQuery,
    PagedList<CarPropertyComboBoxFilterVm>>
{
    private readonly IRepository<Domain.Entities.CarPropertyComboBox> _carPropertyComboBoxRepository;
    private readonly IMapper _mapper;

    public FilterCarPropertyComboBoxQueryHandler(
        IRepository<Domain.Entities.CarPropertyComboBox> carPropertyComboBoxRepository, IMapper mapper)
    {
        _carPropertyComboBoxRepository = carPropertyComboBoxRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<CarPropertyComboBoxFilterVm>> Handle(FilterCarPropertyComboBoxQuery request,
        CancellationToken cancellationToken)
    {
        var comboBoxSpec = new CarPropertyComboBoxSpecification();
        comboBoxSpec.NoTracking();
        comboBoxSpec.Filter(request.ComboBoxFilterParam.Keyword, request.ComboBoxFilterParam.Status);

        var totalRecord = await _carPropertyComboBoxRepository.CountAsync(comboBoxSpec, cancellationToken);
        if (totalRecord <= 0)
            return new PagedList<CarPropertyComboBoxFilterVm>();

        comboBoxSpec.Paging(request.ComboBoxFilterParam.PageNumber, request.ComboBoxFilterParam.PageSize);
        var comboBoxs = await _carPropertyComboBoxRepository.ListAsync(comboBoxSpec, cancellationToken);

        var collections = _mapper.Map<IReadOnlyList<CarPropertyComboBoxFilterVm>>(comboBoxs);
        return new PagedList<CarPropertyComboBoxFilterVm>(request.ComboBoxFilterParam.PageNumber,
            request.ComboBoxFilterParam.PageSize, totalCount: totalRecord, collections);
    }
}