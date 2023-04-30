using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using DVG.AP.Cms.CarInfo.Application.Features.Dtos;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class VariantRepository : Repository<Domain.Entities.Variant>, IVariantRepository
    {
        public VariantRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<FilterVariantVm>> FilterAsync(FilterVariantParameter paramFilter)
        {
            var keywordSearch = ($"%{paramFilter.Keyword}%").ToLower();

            var query = from variant in CarInfoDbContext.Variants
                join model in CarInfoDbContext.Models on variant.ModelId equals model.Id
                join brand in CarInfoDbContext.Brands on model.BrandId equals brand.Id
                where (paramFilter.BrandId == 0 || model.BrandId == paramFilter.BrandId)
                      && (paramFilter.ModelId == 0 || variant.ModelId == paramFilter.ModelId)
                      && (paramFilter.Status == ActiveStatus.All || variant.Status == paramFilter.Status)
                      && (string.IsNullOrEmpty(paramFilter.Keyword) ||
                          EF.Functions.Like(variant.Id.ToString().ToLower(), keywordSearch) || (variant.Name != null &&
                              EF.Functions.Like(variant.Name.ToLower(), keywordSearch)))
                orderby variant.CreatedDate descending, variant.Id descending
                select new FilterVariantVm()
                {
                    Id = variant.Id.ToString(),
                    BrandName = brand.Name,
                    ModelName = model.Name,
                    Name = variant.Name,
                    IsVirtual = variant.IsVirtual,
                    Status = variant.Status
                };

            query = query.AsNoTracking();

            var totalRecord = await query.CountAsync();

            var collections = new List<FilterVariantVm>();
            if (totalRecord > 0)
            {
                collections = await query.Skip((paramFilter.PageNumber - 1) * paramFilter.PageSize)
                    .Take(paramFilter.PageSize).ToListAsync();
            }

            var result = new PagedList<FilterVariantVm>(paramFilter.PageNumber, paramFilter.PageSize, totalRecord,
                collections);
            return result;
        }

        public async Task<VariantDetailVm> GetDetail(int id)
        {
            var query = from variant in CarInfoDbContext.Variants
                join model in CarInfoDbContext.Models on variant.ModelId equals model.Id
                join brand in CarInfoDbContext.Brands on model.BrandId equals brand.Id
                where variant.Id == id
                select new VariantDetailVm()
                {
                    Id = variant.Id,
                    BrandId = brand.Id,
                    ModelId = variant.ModelId,
                    Name = variant.Name,
                    IsVirtual = variant.IsVirtual,
                    Status = variant.Status
                };

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ICollection<Variant>> GetListAsync(int modelId)
        {
            var query = from v in CarInfoDbContext.Variants
                        where (modelId == 0 || v.ModelId == modelId)
                        select v;
            return await query.ToListAsync();
        }

        public async Task<ICollection<Variant>> GetListHaveActiveNewCarAsync(int modelId)
        {
            var query = from v in CarInfoDbContext.Variants
                        join ncv in CarInfoDbContext.NewCarVariants on v.Id equals ncv.VariantId
                        where (modelId == 0 || v.ModelId == modelId)
                        && (ncv.Status == NewCarArticleStatus.Approved && ncv.PublishedDate <= DateTime.Now)
                        select v;
            return await query.ToListAsync();
        }

        public async Task<List<int>> GetListIdByBrandIds(List<int> brandIds)
        {
            var query = from variant in CarInfoDbContext.Variants
                        join model in CarInfoDbContext.Models on variant.ModelId equals model.Id
                        where (brandIds == null || !brandIds.Any() || brandIds.Contains(model.BrandId))
                        select variant.Id;

            return await query.ToListAsync();
        }

        public async Task<List<int>> GetListIdByModelIds(List<int> modelIds)
        {
            var query = CarInfoDbContext.Variants
                .AsNoTracking()
                .Where(v => modelIds == null || !modelIds.Any() || modelIds.Contains(v.ModelId))
                .Select(v => v.Id);
            return await query.ToListAsync();
        }

        public async Task<List<CarStructureDto>> ListCarStructureDtoAsync(List<int>? brandIds = null,
            List<int>? modelIds = null, List<int>? variantIds = null)
        {
            var result = new List<CarStructureDto>();
            if (brandIds != null && brandIds.Any())
            {
                result.AddRange(await (from b in CarInfoDbContext.Brands
                                       where brandIds.Contains(b.Id)
                                       select new CarStructureDto()
                                       {
                                           BrandId = b.Id,
                                           BrandName = b.Name,

                                       }).ToListAsync());
            }

            if (modelIds != null && modelIds.Any())
            {
                result.AddRange(await (from m in CarInfoDbContext.Models
                                       join b in CarInfoDbContext.Brands on m.BrandId equals b.Id
                                       where modelIds.Contains(m.Id)
                                       select new CarStructureDto()
                                       {
                                           BrandId = b.Id,
                                           BrandName = b.Name,
                                           ModelId = m.Id,
                                           ModelName = m.Name
                                       }).ToListAsync());
            }

            if (variantIds != null && variantIds.Any())
            {
                result.AddRange(await (from v in CarInfoDbContext.Variants
                                       join m in CarInfoDbContext.Models on v.ModelId equals m.Id
                                       join b in CarInfoDbContext.Brands on m.BrandId equals b.Id
                                       where variantIds.Contains(v.Id)
                                       select new CarStructureDto()
                                       {
                                           BrandId = b.Id,
                                           BrandName = b.Name,
                                           ModelId = m.Id,
                                           ModelName = m.Name,
                                           VariantId = v.Id,
                                           VariantName = v.Name
                                       }).ToListAsync());
            }

            return result;
        }
    }
}
// .ForMember(des => des.BrandId, opt => opt.MapFrom(src => src.Model.Brand.Id))
//     .ForMember(des => des.BrandName, opt => opt.MapFrom(src => src.Model.Brand.Name))
//     .ForMember(des => des.ModelId, opt => opt.MapFrom(src => src.Model.Id))
//     .ForMember(des => des.ModelName, opt => opt.MapFrom(src => src.Model.Name))
//     .ForMember(des => des.VariantId, opt => opt.MapFrom(src => src.Id))
//     .ForMember(des => des.VariantName, opt => opt.MapFrom(src => src.Name));