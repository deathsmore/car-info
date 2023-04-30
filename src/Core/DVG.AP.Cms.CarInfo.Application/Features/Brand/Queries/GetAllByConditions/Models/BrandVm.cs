using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions.Models
{
    public class BrandVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ActiveStatus Status { get; set; }
    }
}
