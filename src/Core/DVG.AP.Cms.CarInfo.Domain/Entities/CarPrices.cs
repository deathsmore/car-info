using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarPrices")]
    public class CarPrice : EntityBase<int>
    {
        public long CarInfoId { get; set; }
        public int OptionId { get; set; }
        public CarPriceOptionType OptionType { get; set; }
        public double Price { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsPrimary { get; set; }
        public short CurrencyUnitId { get; set; }
    }
}