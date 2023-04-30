using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail.Models
{
    public  class CarPriceForGet
    {
        public CarPriceForGet()
        {

        }

        public CarPriceForGet(CarPrice carPrice)
        {
            Id = carPrice.Id;
            CarInfoId = carPrice.CarInfoId;
            OptionId = carPrice.OptionId;
            OptionType = carPrice.OptionType;
            Price = carPrice.Price;
            IsPrimary = carPrice.IsPrimary;
            CurrencyUnitId = carPrice.CurrencyUnitId;
        }

        public int Id { get; set; }
        public long CarInfoId { get; set; }
        public int OptionId { get; set; }
        public CarPriceOptionType OptionType { get; set; }
        public double Price { get; set; }
        public bool IsPrimary { get; set; }
        public short CurrencyUnitId { get; set; }
    }

    public class CarPriceByLocationVm : CarPriceForGet
    {
        public CarPriceByLocationVm()
        {

        }
        public CarPriceByLocationVm(CarPrice carPrice, District district): base(carPrice)
        {
            DistrictId = district!.Id;
            CityId = district!.City!.Id;
            RegionId = district!.City!.RegionId;
        }
        public int RegionId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
    }
}
