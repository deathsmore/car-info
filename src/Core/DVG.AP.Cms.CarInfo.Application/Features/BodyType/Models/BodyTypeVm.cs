using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.BodyType.Models
{
    public class BodyTypeVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
