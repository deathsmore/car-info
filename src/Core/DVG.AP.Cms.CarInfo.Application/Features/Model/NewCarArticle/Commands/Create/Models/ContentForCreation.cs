using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create.Models
{
    public record ContentForCreation
    {
        public long NewCarArticleId { get; private set; }
        public string? ContentTitle { get; set; }
        public NewCarDetailContentType ContentType { get; set; }
        public short? Order { get; set; }
        public string? LongContent { get; set; }
        public string? GgAmpContent { get; set; }
        public string? AppContent { get; set; }
        public string? FbContent { get; set; }
        /// <summary>
        /// Cho phép ẩn/hiện nội dung
        /// </summary>
        public bool? Visible { get; set; }
    }
}
