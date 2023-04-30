using AutoMapper.QueryableExtensions;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Infrastructures.Base.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class NewCarBrandRepository : Repository<NewCarBrand>, INewCarBrandRepository
    {
        public NewCarBrandRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(List<long> ids, NewCarArticleFilterParam param)
        {
            var excludeIds = param.ExcludeIds is null || !param.ExcludeIds.Any() ? null : param.ExcludeIds!.Select(x => x.ToLong());
            var keywordSearch = ($"%{param.Keyword}%")?.ToLower();
            var query = CarInfoDbContext.NewCarBrands.AsNoTracking()
                //.Include(nc => nc.Model.Brand)
                .Where(nc =>
                    (!ids.Any() || ids.Contains(nc.Id))
                    && (string.IsNullOrEmpty(keywordSearch) ||
                        (EF.Functions.Like(nc.Id.ToString().ToLower(), keywordSearch))
                        || (!string.IsNullOrEmpty(nc.Title) && EF.Functions.Like(nc.Title.ToLower(), keywordSearch))
                    )
                    && (excludeIds == null || !excludeIds.Any() || !excludeIds.Contains(nc.Id))
                    && (param.BrandId == 0 || nc.BrandId == param.BrandId)
                    && (param.Status == NewCarArticleStatus.Default || nc.Status == param.Status)
                    && (param.CreatedBy == 0 || nc.CreatedBy == param.CreatedBy)
                    && (param.ModifiedBy == 0 || nc.ModifiedBy == param.ModifiedBy)
                    && (param.CreatedDateFrom == null || nc.CreatedDate >= param.CreatedDateFrom)
                    && (param.CreatedDateTo == null || nc.CreatedDate <= param.CreatedDateTo))
                //-----------------
                .OrderByDescending(nc => nc.CreatedDate)
                .ProjectTo<NewCarArticleFilterDto>(MapperConfiguration);

            return await GetPagedAsync(param.PageNumber, param.PageSize, query);
        }

        public async Task<NewCarBrand> GetByBrandId(int brandId)
        {
            return await CarInfoDbContext.NewCarBrands
                .FirstOrDefaultAsync(n => n.BrandId == brandId);
        }

        public async Task<NewCarBrand> GetDetailAsync(long id)
        {
            var query = CarInfoDbContext.NewCarBrands
                       .Include(c => c.NewCarFAQs)
                       .Include(c => c.NewCarSEOInfos)
                       .Include(n => n.Contents!.Where(i => i.Type == NewCarArticleType.Brand).OrderBy(c => c.Order))
                        .Where(n => n.Id == id)
                        .Select(nc => nc);
            return await query.FirstOrDefaultAsync();
        }
    }
}
