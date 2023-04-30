using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;


namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface ICarInfoPropertyValueRepository : IRepository<CarInfoPropertyValue>
{
    Task<List<CarInfoPropertyValue>> ListAsync(long carInfoId);
    Task<List<CarInfoPropertyValue>> ListCarInfoSpecByModelAsync(int modelId, bool lastestOnly = false);
    Task<PagedList<CarSpecFilterVm>> FilterAsync(CarSpecFilterParam filterParam);
}