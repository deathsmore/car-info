using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarBestSelling")]
    public class CarBestSelling
    {
        [Key]
        public long Id { get; set; }
        public long CarInfoId { get; set; }
        public int Quality { get; set; }
        public int CityId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}