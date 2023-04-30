using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models
{
    public class CarImageDto
    {
        public int Id { get; set; }
        public long NewCarArticleId { get; set; }
        public string? Url { get; set; }
        public string? AltText { get; set; }
        public int Ordinal { get; set; }
        public string? Title { get; set; }
        public ImageType Type { get; set; }
        public string? ColorCode { get; set; }
        public ImageOfObject ImageOfObject { get; set; }
    }
}
