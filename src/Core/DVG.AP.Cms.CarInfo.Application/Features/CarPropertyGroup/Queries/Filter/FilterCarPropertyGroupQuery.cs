using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.Filter;

public class FilterCarPropertyGroupQuery : IRequest<IReadOnlyList<CarPropertyGroupFilterVm>>
{
    public FilterCarPropertyGroupQuery(CarPropertyGroupFilterParam carPropertyGroupFilterParam)
    {
        CarPropertyGroupFilterParam = carPropertyGroupFilterParam;
    }

    public CarPropertyGroupFilterParam CarPropertyGroupFilterParam { get; }
}

public class CarPropertyGroupFilterParam
{
    public string? KeywordSearch { get; set; }
    public short? Status { get; set; }
}