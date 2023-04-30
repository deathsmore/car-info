using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert.Models
{
    public class CarImageForCreation
    {
        public long ObjectId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public DateTime? CreatedDate => DateTime.Now;
        public int Ordinal { get; set; }
        public string? Title { get; set; } = string.Empty;
        public CarImageType Type { get; set; }
        public string ColorCode { get; set; } = string.Empty;
        public ImageOfObject ImageOfObject { get; set; } = ImageOfObject.ImageOfCarInfo;
    }
}
