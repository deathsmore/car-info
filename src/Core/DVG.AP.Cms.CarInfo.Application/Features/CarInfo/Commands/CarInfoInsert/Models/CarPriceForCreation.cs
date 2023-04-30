using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert.Models
{
    public  class CarPriceForCreation
    {
        public long CarInfoId { get; private set; }
        public int OptionId { get; set; }
        public CarPriceOptionType OptionType { get; set; }
        public double Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate => DateTime.Now;
        public bool IsPrimary { get; set; }
        public short CurrencyUnitId { get; set; }
    }
}
