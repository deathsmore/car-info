using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class CarInfoPropertyValueRepository : Repository<CarInfoPropertyValue>,
    ICarInfoPropertyValueRepository
{
    public CarInfoPropertyValueRepository(CarInfoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<CarInfoPropertyValue>> ListAsync(long carInfoId)
    {
        var query = CarInfoDbContext.CarInfoPropertyValues.Where(cp => cp.CarInfoId == carInfoId)
            .AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<PagedList<CarSpecFilterVm>> FilterAsync(CarSpecFilterParam filterParam)
    {
        var carInfoId = filterParam.CarInfoId.ToLong();
        var query = CarInfoDbContext.CarInfoPropertyValues
            .Include(cp => cp.CarInfo.Variant.Model.Brand)
            .Where(cp =>
                (filterParam.Year == 0 || cp.CarInfo.Year == filterParam.Year)
                && (filterParam.BrandId == 0 || cp.CarInfo.Variant.Model.BrandId == filterParam.BrandId)
                && (filterParam.ModelId == 0 || cp.CarInfo.Variant.ModelId == filterParam.ModelId)
                && (carInfoId == 0 || cp.CarInfoId == carInfoId)
            )
            .OrderByDescending(cp => cp.CreatedDate)
            .Select(cp => new CarSpecFilterVm(cp.CarInfoId, cp.CarInfo.Variant.Model.Brand.Name,
                cp.CarInfo.Variant.Model.Name, cp.CarInfo.Name, cp.CarInfo.Year, cp.CarInfo.Variant.Model.BrandId,
                cp.CarInfo.Variant.ModelId)
            ).AsNoTracking();
        var totalRecord = await query.CountAsync();
        if (totalRecord <= 0) return new PagedList<CarSpecFilterVm>();
        var rowSkip = filterParam.PageNumber - 1;
        var collections = await query.Skip(rowSkip * filterParam.PageSize)
            .Take(filterParam.PageSize).ToListAsync();
        return new PagedList<CarSpecFilterVm>(filterParam.PageNumber, filterParam.PageSize, totalRecord,
            collections);
    }

    public async Task<List<CarInfoPropertyValue>> ListCarInfoSpecByModelAsync(int modelId, bool lastestOnly = false)
    {
        var query = CarInfoDbContext.CarInfoPropertyValues
            .Where(cp => cp.CarInfo.Status == Domain.Enums.ActiveStatus.Active
                    && cp.CarInfo.Variant.ModelId == modelId
                    && cp.CarProperty.IsModelSpec == true
            )
            .AsNoTracking();
        if(lastestOnly)
        {
            query = query.Where(cp => cp.CarInfo.IsLatest);
        }    

        return await query.ToListAsync();
    }
}