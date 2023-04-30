using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.CheckExist
{
    public class CheckExistQuery : IRequest<bool>
    {
        public string ObjectId { get; set; }
        public NewCarArticleType Type { get; set; }
    }
}
