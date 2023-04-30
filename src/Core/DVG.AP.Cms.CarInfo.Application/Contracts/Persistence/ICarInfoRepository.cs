using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetInfoForCreatePromotion;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using System.Linq.Expressions;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface ICarInfoRepository : IRepository<Domain.Entities.CarInfo>
{
    Task<Domain.Entities.CarInfo?> GetDetailIncludePropertyValue(long id);
    Task<PagedList<CarSpecFilterVm>> GetPagedListCarSpec(CarSpecFilterParam param);
    Task<PagedList<FilterCarInfoVm>> FilterAsync(FilterCarInfoParameter paramFilter);
    Task<Domain.Entities.CarInfo> GetLatest(int variantId);
    Task<int> UnsetLatest(Domain.Entities.CarInfo carInfo);
    /// <summary>
    /// Lây danh sách carInfo với  thông tin (only) minPrice. maxPrice thuộc model
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByModel(int modelId);

    /// <summary>
    /// Lây danh sách carInfo với  thông tin (only) minPrice. maxPrice thuộc brand
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByBrand(int brandId);
    //Task<IReadOnlyList<Domain.Entities.CarInfo>> GetAll();

    /// <summary>
    /// Lây danh sách carInfo với  thông tin (only) minPrice. maxPrice thuộc model
    /// </summary>
    /// <param name="modelId"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByVariant(int variantId);
    Task<CarInfoForCreatePromotionVm> GetInfoForCreatePromotion(long id);
    Task<IReadOnlyList<TEntity>> ListAsync<TEntity>(int top, string? orderBy, Expression<Func<Domain.Entities.CarInfo, TEntity>> returnObject,
        params Expression<Func<Domain.Entities.CarInfo, bool>>[]? conditions)
        where TEntity : class;
    Task<double> GetListedPrice(long id);
}