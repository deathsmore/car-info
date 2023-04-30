using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;

using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.Filter;

public class FilterNewCarArticleQuery : IRequest<PagedList<NewCarArticleFilterDto>>
{
    public FilterNewCarArticleQuery(NewCarArticleFilterParam newCarArticleFilterParam)
    {
        NewCarArticleFilterParam = newCarArticleFilterParam;
    }

    public NewCarArticleFilterParam NewCarArticleFilterParam { get; set; }
}