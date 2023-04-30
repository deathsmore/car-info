using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models
{
    public class ContentForUpdate
    {
        public int Id { get; set; }
        public string ContentTitle { get; set; }
        public NewCarDetailContentType ContentType { get; set; }
        public short? Order { get; set; }
        public string LongContent { get; set; } = string.Empty;
        public string GgAmpContent { get; set; } = string.Empty;
        public string AppContent { get; set; } = string.Empty;
        public string FbContent { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
    }
}
