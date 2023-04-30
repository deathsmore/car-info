using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models
{
    public class ImageForUpdate
    {
        public int Id { get; set; }
        public string? Url { get; set; } = string.Empty;
        public long NewCarArticleId { get; set; }
        public string? AltText { get; set; } = string.Empty;
        public DateTime ModifiedDate => DateTime.Now;
        public int Ordinal { get; set; }
        public string? Title { get; set; }
        public ImageType Type { get; set; }
        public string? ColorCode { get; set; }
        
    }
}