

using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface ICarColorRepository : IRepository<Domain.Entities.CarColor>
    {
        Task<PagedList<FilterCarColorVm>> FilterAsync(FilterCarColorParameter paramFilter);
    }
}
