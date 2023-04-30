using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate.Models
{
    public class CarImageForUpdate
    {
        public int Id { get; set; }
        public long CarInfoId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public DateTime? ModifiedDate => DateTime.Now;
        public int Ordinal { get; set; }
        public string? Title { get; set; } = string.Empty;
        public CarImageType Type { get; set; }
        public string ColorCode { get; set; } = string.Empty;
    }
}
