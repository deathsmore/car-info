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
    public class NewCarModelRepository : Repository<NewCarModel>, INewCarModelRepository
    {
        public NewCarModelRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CountActiveArticlesByBrandAsync(int brandId)
        {
            var count = await CarInfoDbContext.NewCarModels
                                .Where(nc =>
                                    nc.BrandId == brandId
                                    && nc.Status == NewCarArticleStatus.Approved
                                    && nc.PublishedDate <= DateTime.Now
                                ).CountAsync();
            return count;
        }

        public async Task<PagedList<NewCarArticleFilterDto>> FilterAsync(List<long> ids, NewCarArticleFilterParam param)
        {
            var excludeIds = param.ExcludeIds is null || !param.ExcludeIds.Any() ? null : param.ExcludeIds!.Select(x => x.ToLong());
            var keywordSearch = ($"%{param.Keyword}%")?.ToLower();
            var query = CarInfoDbContext.NewCarModels.AsNoTracking()
                //.Include(nc => nc.Model.Brand)
                .Where(nc =>
                    (!ids.Any() || ids.Contains(nc.Id))
                    && (string.IsNullOrEmpty(keywordSearch) ||
                        (EF.Functions.Like(nc.Id.ToString().ToLower(), keywordSearch))
                        || (!string.IsNullOrEmpty(nc.Title) && EF.Functions.Like(nc.Title.ToLower(), keywordSearch))
                    )
                    && (excludeIds == null || !excludeIds.Any() || !excludeIds.Contains(nc.Id))
                    && (param.BrandId == 0 || nc.BrandId == param.BrandId)
                    && (param.ModelId == 0 || nc.ModelId == param.ModelId)
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

        public async Task<NewCarModel> GetByModelId(int modelId)
        {
            return await CarInfoDbContext.NewCarModels.FirstOrDefaultAsync(n => n.ModelId == modelId);
        }

        public async Task<NewCarModel> GetDetailAsync(long id)
        {
            var query = CarInfoDbContext.NewCarModels
                        .Include(n => n.Contents!.Where(i => i.Type == NewCarArticleType.Model).OrderBy(c => c.Order))
                        .Include(n => n.Images!.Where(i => i.ImageOfObject == ImageOfObject.ImageOfNewCarModel))
                        .Include(c => c.NewCarFAQs)
                        .Include(c => c.NewCarSEOInfos)
                        .Where(n => n.Id == id)
                        .Select(nc => nc);
            return await query.FirstOrDefaultAsync();
        }
    }
}
