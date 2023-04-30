using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AutoPortal.Core.Infrastructures.Base;

using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;

public class FilterCarSpectQuery : IRequest<PagedList<CarSpecFilterVm>>
{
    public CarSpecFilterParam CarSpectFilterParam { get; set; } = new CarSpecFilterParam();
}

public class CarSpecFilterParam : PagingParam
{
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int VariantId { get; set; }
    public string? CarInfoId { get; set; }

    public int Year { get; set; }
}