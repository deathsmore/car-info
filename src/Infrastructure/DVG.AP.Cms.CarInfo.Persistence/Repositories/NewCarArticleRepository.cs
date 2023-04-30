using AutoMapper.QueryableExtensions;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class NewCarArticleRepository : Repository<NewCarArticle>, INewCarArticleRepository
{

    public NewCarArticleRepository(CarInfoDbContext newCarArticleDbContext) : base(newCarArticleDbContext)
    {
    }

    // public async Task<NewCarArticleGetDetailDto?> GetDetailAsync(long id)
    // {
    //     var query = CarInfoDbContext.NewCarArticles.AsSplitQuery()
    //         .Include(nc => nc.Contents)
    //         .Include(nc => nc.CarImages.Where(ci => ci.ImageOfObject == ImageOfObject.ImageOfNewCar))
    //         .Where(nc => (nc.Id == id))
    //         .AsNoTracking()
    //         .ProjectTo<NewCarArticleGetDetailDto>(MapperConfiguration);
    //     return await query.AsNoTracking().FirstOrDefaultAsync();
    // }

    public async Task<IReadOnlyList<NewCarArticleFilterDto>> GetListAsync()
    {
        var query = CarInfoDbContext.NewCarArticles.ProjectTo<NewCarArticleFilterDto>(MapperConfiguration)
            .AsNoTracking();
        return await query.ToListAsync();
    }

    public async Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleBrandAsync(List<long> ids,
        NewCarArticleFilterParam param)
    {
        var query = BuildQueryFilterBase(ids, param)
            .Where(nc =>
                (nc.Type == NewCarArticleType.Brand)
                && (param.BrandId == 0 || nc.ObjectId == param.BrandId))
            .OrderByDescending(nc => nc.CreatedDate)
            .ProjectTo<NewCarArticleFilterDto>(MapperConfiguration)
            .AsNoTracking();
        return await GetPagedAsync(param.PageNumber, param.PageSize, query);
    }

    public async Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleModelAsync(List<long> ids,
        NewCarArticleFilterParam param)
    {
        var query = BuildQueryFilterBase(ids, param)
            .Where(nc =>
                (nc.Type == NewCarArticleType.Model)
                && (param.BrandId == 0 || nc.ParentOfObjectId == param.BrandId)
                && (param.ModelId == 0 || nc.ObjectId == param.ModelId))
            .OrderByDescending(nc => nc.CreatedDate)
            .ProjectTo<NewCarArticleFilterDto>(MapperConfiguration)
            .AsNoTracking();

        return await GetPagedAsync(param.PageNumber, param.PageSize, query);
    }

    public async Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleCarInfoAsync(List<long> ids,
        NewCarArticleFilterParam param, List<int>? variantIds = null)
    {
        var variantIdLongs = variantIds == null ? new List<long>() : variantIds.Select(Convert.ToInt64).ToList();
        var query = BuildQueryFilterBase(ids, param)
            .Where(nc =>
                (nc.Type == NewCarArticleType.Variant)
                && (variantIds!= null && variantIdLongs.Any() && variantIdLongs.Contains(nc.ObjectId)))
            .OrderByDescending(nc => nc.CreatedDate)
            .ProjectTo<NewCarArticleFilterDto>(MapperConfiguration)
            .AsNoTracking();
        return await GetPagedAsync(param.PageNumber, param.PageSize, query);
    }

    public async Task<PagedList<NewCarArticleFilterDto>> GetPagedListAllTypeAsync(List<long> ids,
        NewCarArticleFilterParam param,
        List<int>? variantIds = null)
    {
        var variantIdLongs = variantIds == null ? new List<long>() : variantIds.Select(Convert.ToInt64).ToList();

        var query = BuildQueryFilterBase(ids, param)
            .Where(nc =>
                //Lấy bài brand
                (
                    nc.Type == NewCarArticleType.Brand
                    && (param.BrandId == 0 || nc.ObjectId == param.BrandId)
                )
                ||
                //Lấy bài model
                (
                    nc.Type == NewCarArticleType.Model
                    && (
                        (param.ModelId == 0 || nc.ObjectId == param.ModelId)
                        && (param.BrandId == 0 || nc.ParentOfObjectId == param.BrandId)
                    )
                )
                ||
                //Lấy bài variant
                (
                    nc.Type == NewCarArticleType.Variant
                    && (!variantIdLongs.Any() || variantIdLongs.Contains(nc.ObjectId))
                )
            )
            .OrderByDescending(nc => nc.CreatedDate)
            .ProjectTo<NewCarArticleFilterDto>(MapperConfiguration)
            .AsNoTracking();

        return await GetPagedAsync(param.PageNumber, param.PageSize, query);
    }

    #region Private Method

    private IQueryable<NewCarArticle> BuildQueryFilterBase(ICollection<long> ids, NewCarArticleFilterParam param)
    {
        var excludeIds = param.ExcludeIds is null || !param.ExcludeIds.Any() ? null : param.ExcludeIds!.Select(x=> x.ToLong());
        var keywordSearch = ($"%{param.Keyword}%")?.ToLower();
        var query = CarInfoDbContext.NewCarArticles.AsNoTracking()
            .Where(nc =>
                (!ids.Any() || ids.Contains(nc.Id))
                && (string.IsNullOrEmpty(keywordSearch) ||
                    (EF.Functions.Like(nc.Id.ToString().ToLower(), keywordSearch))
                    || (!string.IsNullOrEmpty(nc.Title) && EF.Functions.Like(nc.Title.ToLower(), keywordSearch))
                )
                && (excludeIds == null || !excludeIds.Any() || !excludeIds.Contains(nc.Id))
                && (param.Status == NewCarArticleStatus.Default || nc.Status == param.Status)
                && (param.CreatedBy == 0 || nc.CreatedBy == param.CreatedBy)
                && (param.ModifiedBy == 0 || nc.ModifiedBy == param.ModifiedBy)
                && (param.CreatedDateFrom == null || nc.CreatedDate >= param.CreatedDateFrom)
                && (param.CreatedDateTo == null || nc.CreatedDate <= param.CreatedDateTo));
        return query;
    }

    public async Task<NewCarArticle> GetByObjectIdAndType(long objectId, NewCarArticleType type)
    {
        return await CarInfoDbContext.NewCarArticles.FirstOrDefaultAsync(n => n.ObjectId == objectId && n.Type == type);
    }

    #endregion
}