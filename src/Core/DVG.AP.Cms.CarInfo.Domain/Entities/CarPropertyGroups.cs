using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarPropertyGroups")]
    public class CarPropertyGroup
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public short Status { get; set; }
        public int Ordinal { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public IEnumerable<CarProperty> CarProperties { get; set; } = new List<CarProperty>();
    }
}