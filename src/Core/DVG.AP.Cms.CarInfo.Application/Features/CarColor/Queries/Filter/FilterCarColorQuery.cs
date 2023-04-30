
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter
{
    public class FilterCarColorQuery : IRequest<PagedList<FilterCarColorVm>>
    {
        public FilterCarColorParameter CarColorParameter { get; set; }
    }
}
