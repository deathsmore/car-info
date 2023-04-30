using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AutoPortal.Core.Infrastructures.Utilies;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface ICarPropertyRepository : IRepository<CarProperty>
{
    Task<PagedList<CarSpecFilterVm>> FilterCarSpecAsync(CarSpecFilterParam? param);
    Task<IReadOnlyList<CarProperty>> GetModelSpecs();
}