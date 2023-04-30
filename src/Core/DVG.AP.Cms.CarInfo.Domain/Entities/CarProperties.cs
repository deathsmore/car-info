// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarProperties")]
    public class CarProperty
    {
        [Key] public int Id { get; set; }
        public int CarPropertyGroupId { get; set; }
        public int CarPropertyComboBoxId { get; set; }
        public int MaxLength { get; set; }
        public int Ordinal { get; set; }
        public CarPropertyType Type { get; set; }
        public short Status { get; set; }
        public string? Name { get; set; }
        public string? DefaultValue { get; set; }
        public PropertyUnit Unit { get; set; }
        public bool IsRequired { get; set; }
        public bool IsMultiChoice { get; set; }
        public bool IsModelSpec { get; set; }
        public bool IsCrawled { get; set; }
    }
}