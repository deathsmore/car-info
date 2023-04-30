using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface INewCarArticleRepository : IRepository<NewCarArticle>
{
    Task<NewCarArticle> GetByObjectIdAndType(long objectId, NewCarArticleType type);
    Task<PagedList<NewCarArticleFilterDto>> GetPagedListAllTypeAsync(List<long> ids,
        NewCarArticleFilterParam param, List<int>? variantIds = null);
    Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleBrandAsync(List<long> ids,
        NewCarArticleFilterParam param);


    Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleModelAsync(List<long> ids,
        NewCarArticleFilterParam param);

    Task<PagedList<NewCarArticleFilterDto>> GetPagedListNewCarArticleCarInfoAsync(List<long> ids,
        NewCarArticleFilterParam param, List<int>? variantIds = null);
}