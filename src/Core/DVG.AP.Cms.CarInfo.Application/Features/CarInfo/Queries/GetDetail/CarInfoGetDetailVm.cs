using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail
{
    public class CarInfoGetDetailVm
    {
        public string Id { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int VariantId { get; set; }
        public int BodyTypeId { get; set; }
        public string? BodyTypeName { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Url { get; set; }
        public int Year { get; set; }
        public string? Engine { get; set; }
        
        public short TransmissionId { get; set; }
        public string? TransmissionName { get; set; }
        public short FuelTypeId { get; set; }
        public string? FuelTypeName { get; set; }
        public short NumOfSeat { get; set; }
        public double MaxPower { get; set; }
        public double MaxTorque { get; set; }
        public short NumOfDoor { get; set; }
        public DateTime? LaunchedDate { get; set; }
        public ActiveStatus Status { get; set; }
        public bool IsDiscontinued { get; set; }
       
        public IEnumerable<CarPriceForGet> Prices { get; set; }
        public IEnumerable<CarImageForGet> Images { get; set; }
        public IEnumerable<CarPriceByLocationVm> PricesByLocation { get; set; }
    }
}