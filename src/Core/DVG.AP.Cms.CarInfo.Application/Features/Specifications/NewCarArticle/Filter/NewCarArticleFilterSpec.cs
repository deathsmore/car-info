using DVG.AP.Cms.CarInfo.Application.Contracts.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Dtos;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle.Filter;

public abstract class NewCarArticleFilterSpec : Specification<Domain.Entities.NewCarArticle, NewCarArticleDto>
{
    public void SetNoTracking()
    {
        Query.AsNoTracking();
    }

    public void Paging(int page, int pageSize)
    {
        Query
            .OrderByDescending(nc => nc.ModifiedDate)
            .Skip(PaginationHelper.CalculateSkip(pageSize, page))
            .Take(PaginationHelper.CalculateTake(pageSize));
    }

   

   

    public void GetByIds(List<long> newcarIds)
    {
        if (!newcarIds.Any())
        {
            return;
        }

        Query.AsNoTracking().Where(n => newcarIds.Contains(n.Id));
    }
}