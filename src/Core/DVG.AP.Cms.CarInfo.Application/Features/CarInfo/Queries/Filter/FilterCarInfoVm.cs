using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter
{
    public class FilterCarInfoVm
    {
        public FilterCarInfoVm()
        {

        }
        public FilterCarInfoVm(Domain.Entities.CarInfo carInfo, Domain.Entities.Brand brand,
            Domain.Entities.Model model)
        {
            Id = carInfo.Id.ToString();
            Name = carInfo.Name;
            Year = carInfo.Year;
            Status = carInfo.Status;
            BrandName = brand.Name;
            ModelName = model.Name;
        }

        public string Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string? ModelName { get; set; }
        public string? Name { get; set; }
        public int Year { get; set; }
        public ActiveStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}