using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail.Models
{
    public class CarImageForGet
    {
        public int Id { get; set; }
        public long CarInfoId { get; set; }
        public string? Url { get; set; } 
        public string? AltText { get; set; } 
        public int Ordinal { get; set; }
        public string? Title { get; set; }
        public CarImageType Type { get; set; }
        public string? ColorCode { get; set; }
    }
}
