using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using MediatR;


namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter
{
    public class FilterCarInfoQuery : IRequest<PagedList<FilterCarInfoVm>>
    {
        public FilterCarInfoParameter? CarInfoParameter { get; set; }
    }
}
