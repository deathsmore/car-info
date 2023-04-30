using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter;
using Microsoft.EntityFrameworkCore;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AP.Cms.CarInfo.Persistence.Helpers.Extensions;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetInfoForCreatePromotion;
using System.Linq.Expressions;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Helpers.OrderHelper;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class CarInfoRepository : Repository<Domain.Entities.CarInfo>, ICarInfoRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public CarInfoRepository(CarInfoDbContext dbContext, IPropertyMappingService propertyMappingService) : base(dbContext)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Domain.Entities.CarInfo?> GetDetailIncludePropertyValue(long id)
        {
            var query = CarInfoDbContext.CarInfos.AsNoTracking()
                    .Include(ci => ci.Variant.Model)
                    .Include(ci => ci.CarInfoPropertyValues)
                    .FirstOrDefaultAsync(ci => ci.Id == id)
                ;
            return await query;
        }

        // Query
        //     .Select(ci => new CarSpecFilterVm()
        // {
        //     CarInfoId = ci.Id.ToString(),
        //     //BrandId = ci.Model.BrandId,//T-TEMP
        //     //BrandName = ci.Model.Brand.Name,//T-TEMP
        //     //ModelName = ci.Model.Name,//T-TEMP
        //     CarInfoName = ci.Name,
        //     //ModelId = ci.Model.Id,//T-TEMP
        //     Year = ci.Year,
        // })
        // .Include(c => c.CarInfoPropertyValues)
        //     //.Include(c => c.Model)
        //     //.ThenInclude(m => m.Brand)//T-TEMP
        //     .OrderByDescending(ci => ci.CreatedDate)
        //     .Where(ci => (carInfoId == 0 || ci.Id == carInfoId))
        // .Where(ci =>
        // (ci.CarInfoPropertyValues.Any() &&
        // (param.Year == 0 || ci.Year == param.Year)))
        // //.Where(ci => (param.BrandId == 0 || ci.Model.BrandId == param.BrandId))//T-TEMP
        // //.Where(ci => (param.ModelId == 0 || ci.ModelId == param.ModelId))//T-TEMP
        //
        // .AsNoTracking();
        public async Task<PagedList<CarSpecFilterVm>> GetPagedListCarSpec(CarSpecFilterParam param)
        {
            var carInfoId = param.CarInfoId.ToLong();
            var query = CarInfoDbContext.CarInfos
                .Include(ci => ci.Variant.Model.Brand)
                .Include(ci => ci.CarInfoPropertyValues)
                .Where(ci =>
                    (carInfoId == 0 || carInfoId == ci.Id)
                    && (param.Year == 0 || param.Year == ci.Year)
                    && (param.BrandId == 0 || ci.Variant.Model.BrandId == param.BrandId)
                    && (param.ModelId == 0 || param.ModelId == ci.Variant.ModelId)
                    && (param.VariantId == 0 || param.VariantId == ci.VariantId)
                    && (ci.CarInfoPropertyValues.Any())
                ).OrderByDescending(ci => ci.CreatedDate)
                .Select(ci => new CarSpecFilterVm(ci.Id, ci.Variant.Model.Brand.Name, ci.Variant.Model.Name, ci.Name,
                    ci.Year, ci.Variant.Model.BrandId, ci.Variant.ModelId)
                ).AsNoTracking();
            var totalRecord = await query.CountAsync();
            if (totalRecord <= 0)
                return new PagedList<CarSpecFilterVm>();
            var rowSkip = param.PageNumber - 1;
            var collections = await query.Skip(rowSkip * param.PageSize).Take(param.PageSize).ToListAsync();
            return new PagedList<CarSpecFilterVm>(param.PageNumber, param.PageSize, totalRecord, collections);
        }

        public async Task<PagedList<FilterCarInfoVm>> FilterAsync(FilterCarInfoParameter paramFilter)
        {
            var keywordSearch = ($"%{paramFilter.Keyword}%").ToLower();

            var query = from carInfo in CarInfoDbContext.CarInfos
                        join variant in CarInfoDbContext.Variants on carInfo.VariantId equals variant.Id
                        join model in CarInfoDbContext.Models on variant.ModelId equals model.Id
                        join brand in CarInfoDbContext.Brands on model.BrandId equals brand.Id
                        where (paramFilter.BrandId == 0 || model.BrandId == paramFilter.BrandId)
                              && (paramFilter.ModelId == 0 || variant.ModelId == paramFilter.ModelId)
                              && (paramFilter.Status == ActiveStatus.All || carInfo.Status == paramFilter.Status)
                              && (string.IsNullOrEmpty(paramFilter.Keyword) ||
                                  EF.Functions.Like(carInfo.Id.ToString().ToLower(), keywordSearch) || (carInfo.Name != null &&
                                      EF.Functions.Like(carInfo.Name.ToLower(), keywordSearch)))
                        orderby carInfo.CreatedDate descending, carInfo.ModifiedDate descending, carInfo.Id descending
                        select new FilterCarInfoVm()
                        {
                            Id = carInfo.Id.ToString(),
                            Name = carInfo.Name,
                            Year = carInfo.Year,
                            Status = carInfo.Status,
                            CreatedBy = carInfo.CreatedBy,
                            CreatedDate = carInfo.CreatedDate.HasValue ? (DateTime)carInfo.CreatedDate : DateTime.MinValue,
                            ModifiedBy = carInfo.ModifiedBy,
                            ModifiedDate = carInfo.ModifiedDate.HasValue ? (DateTime)carInfo.ModifiedDate : DateTime.MinValue,
                            BrandName = brand.Name,
                            ModelName = model.Name,
                        };
                        //select new FilterCarInfoVm(carInfo, brand, model);

            query = query.AsNoTracking();

            var totalRecord = await query.CountAsync();
            if (totalRecord <= 0)
                return new PagedList<FilterCarInfoVm>();

            var carInfoDtos = await query.Skip((paramFilter.PageNumber - 1) * paramFilter.PageSize)
                .Take(paramFilter.PageSize).ToListAsync();
            var result = new PagedList<FilterCarInfoVm>(paramFilter.PageNumber, paramFilter.PageSize, totalRecord,
                carInfoDtos);
            return result;
        }

        public async Task<Domain.Entities.CarInfo> GetLatest(int variantId)
        {
            var query = from carInfo in CarInfoDbContext.CarInfos
                where carInfo.VariantId == variantId
                      && carInfo.IsLatest
                select carInfo;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> UnsetLatest(Domain.Entities.CarInfo carInfo)
        {
            carInfo.IsLatest = false;

            CarInfoDbContext.CarInfos.Update(carInfo);
            var affectUpdate = await CarInfoDbContext.SaveChangesAsync();
            return affectUpdate;
        }

        public async Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByModel(int modelId)
        {
            var query = CarInfoDbContext.CarInfos
                .AsNoTracking()
                .AsSplitQuery()
                .Include(carInfo => carInfo.Variant.Model)
                .Where(c => c.Variant.ModelId == modelId)
                .ApplyRuleDisplayWithModel()
                .Select(c => new Domain.Entities.CarInfo()
                {
                    Id = c.Id,
                    MinPrice = c.MinPrice,
                    MaxPrice = c.MaxPrice
                });
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByBrand(int brandId)
        {
            var query = CarInfoDbContext.CarInfos
                .AsNoTracking()
                .AsSplitQuery()
                .Include(carInfo => carInfo.Variant.Model.Brand)
                .Where(c => c.Variant.Model.BrandId == brandId)
                .ApplyRuleDisplayWithBrand()
                .Select(c => new Domain.Entities.CarInfo()
                {
                    Id = c.Id,
                    MinPrice = c.MinPrice,
                    MaxPrice = c.MaxPrice
                });
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<Domain.Entities.CarInfo>> GetListPriceRangeByVariant(int variantId)
        {
            var query = CarInfoDbContext.CarInfos
                .AsNoTracking()
                .AsSplitQuery()
                .Where(c => c.VariantId == variantId)
                .ApplyRuleDisplayWithModel()
                .Select(c => new Domain.Entities.CarInfo()
                {
                    Id = c.Id,
                    MinPrice = c.MinPrice,
                    MaxPrice = c.MaxPrice,
                    IsLatest = c.IsLatest,
                });
            return await query.ToListAsync();
        }

        public async Task<CarInfoForCreatePromotionVm> GetInfoForCreatePromotion(long id)
        {
            var query = CarInfoDbContext.CarInfos.AsNoTracking()
                    .Include(ci => ci.Prices)
                    .Where(ci => ci.Id == id)
                    .Select(ci => new CarInfoForCreatePromotionVm()
                    {
                        Avatar = ci.Avatar,
                        ListedPrice = ci.Prices.FirstOrDefault(x => x.IsPrimary) != null ? ci.Prices.FirstOrDefault(x => x.IsPrimary).Price : 0D
                    });
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TEntity>> ListAsync<TEntity>(int top, string? orderBy, Expression<Func<Domain.Entities.CarInfo, TEntity>> returnObject, params Expression<Func<Domain.Entities.CarInfo, bool>>[]? conditions) where TEntity : class
        {
            var selectQuery = BuildSelectQuery(orderBy, returnObject, conditions);  
            var collection = await (top > 0 ? selectQuery.Take(top).AsSplitQuery().ToListAsync() : selectQuery.AsSplitQuery().ToListAsync());
            return collection;
        }

        private IQueryable<TEntity> BuildSelectQuery<TEntity>(string? orderBy, Expression<Func<Domain.Entities.CarInfo, TEntity>> returnObject, params Expression<Func<Domain.Entities.CarInfo, bool>>[]? conditions) where TEntity : class
        {
            //if (!_propertyMappingService.ValidMappingExistsFor<Domain.Entities.CarInfo, Domain.Entities.CarInfo>
            //        (orderBy) || string.IsNullOrEmpty(orderBy))
            //{
            //    orderBy = StaticVariables.PromotionInBoxSetting?.OrderByDefault;
            //}

            var query = CarInfoDbContext.Set<Domain.Entities.CarInfo>().AsNoTracking();
            if (conditions is { Length: > 0 })
            {
                query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
            }

            var carInfoPropertyMapping = _propertyMappingService
                .GetPropertyMapping<Domain.Entities.CarInfo, Domain.Entities.CarInfo>();
            var selectQuery = query.Select(returnObject);

            selectQuery = selectQuery.ApplySort(orderBy: orderBy, mappingDictionary: carInfoPropertyMapping);
            return selectQuery;
        }

        public async Task<double> GetListedPrice(long id)
        {
            var query = CarInfoDbContext.CarPrices.AsNoTracking()
                .Where(cp => cp.CarInfoId == id && cp.IsPrimary)
                .Select(cp => cp.Price);

            return await query.FirstOrDefaultAsync();
        }
    }
}