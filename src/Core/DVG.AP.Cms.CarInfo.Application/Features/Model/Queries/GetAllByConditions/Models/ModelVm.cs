using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models
{
    public class ModelVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
