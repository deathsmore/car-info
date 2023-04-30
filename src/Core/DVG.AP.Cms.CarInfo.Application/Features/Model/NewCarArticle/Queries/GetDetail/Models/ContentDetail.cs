using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models
{
    public class ContentDetailDto
    {
        public int Id { get; set; }
        public string NewCarArticleId { get; set; } = string.Empty;
        public string? ContentTitle { get; set; }
        public NewCarDetailContentType ContentType { get; set; }
        public short Order { get; set; }
        public string? LongContent { get; set; }
        public string? GgAmpContent { get; set; }
        public string? AppContent { get; set; }
        public string? FbContent { get; set; }
        public bool Visible { get; set; }
    }
}