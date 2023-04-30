using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate.Models
{
    public  class CarPriceForUpdate
    {
        public int Id { get; set; }
        public long CarInfoId { get; set; }
        public int OptionId { get; set; }
        public CarPriceOptionType OptionType { get; set; }
        public double Price { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate => DateTime.Now;
        public bool IsPrimary { get; set; }
        public short CurrencyUnitId { get; set; }
    }
}
