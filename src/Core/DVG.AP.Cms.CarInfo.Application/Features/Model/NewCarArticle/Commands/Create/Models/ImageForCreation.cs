using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create.Models
{
    public record ImageForCreation
    {
        public long ObjectId { get; set; }
        public string? Url { get; set; }
        public string? AltText { get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public DateTime ModifiedDate => DateTime.Now;
        public int Ordinal { get; set; }
        public string? Title { get; set; }
        public ImageType Type { get; set; }
        public string? ColorCode { get; set; }
        public ImageOfObject ImageOfObject { get; set; }
    }
}
