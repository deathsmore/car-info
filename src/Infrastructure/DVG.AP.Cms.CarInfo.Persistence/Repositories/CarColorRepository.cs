using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class CarColorRepository : Repository<CarColor>,
    ICarColorRepository
{
    public CarColorRepository(CarInfoDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<FilterCarColorVm>> FilterAsync(FilterCarColorParameter paramFilter)
    {
        var keywordSearch = ($"%{paramFilter.Keyword}%").ToLower();

        var query = from carColor in CarInfoDbContext.CarColors
            where (string.IsNullOrEmpty(paramFilter.Keyword) ||
                   (carColor.Code != null && EF.Functions.Like(carColor.Code.ToLower(), keywordSearch) ||
                    (carColor.Name != null && EF.Functions.Like(carColor.Name.ToLower(), keywordSearch))))
            select new FilterCarColorVm(carColor);

        query = query.AsNoTracking();
        var totalRecord = await query.CountAsync();
        var carColorDtos = new List<FilterCarColorVm>();
        if (totalRecord > 0)
        {
            carColorDtos = await query.Skip((paramFilter.PageNumber - 1) * paramFilter.PageSize)
                .Take(paramFilter.PageSize).ToListAsync();
        }

        var result = new PagedList<FilterCarColorVm>(paramFilter.PageNumber, paramFilter.PageSize, totalRecord,
            carColorDtos);
        return result;
    }
}