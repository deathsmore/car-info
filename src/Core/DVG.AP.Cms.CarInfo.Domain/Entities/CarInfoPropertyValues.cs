using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarInfoPropertyValues")]
    public class CarInfoPropertyValue
    {
        [Key] public int Id { get; set; }
        public string? Value { get; set; }
        public double NumberValue { get; set; }
        public DateTime? DateValue { get; set; }
        public int[]? ListValue { get; set; }
        public int CarPropertyId { get; set; }
        public int CarPropertyComboBoxId { get; set; }
        public long CarInfoId { get; set; }
        public CarInfo CarInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public CarProperty CarProperty { get; set; }
    }
}