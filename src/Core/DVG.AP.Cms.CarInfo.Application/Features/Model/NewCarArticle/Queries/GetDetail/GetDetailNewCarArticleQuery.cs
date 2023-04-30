using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;

public class GetDetailNewCarArticleQuery : IRequest<NewCarArticleGetDetailDto>
{
    public long Id { get; set; }
    public NewCarArticleType Type { get; set; }
}