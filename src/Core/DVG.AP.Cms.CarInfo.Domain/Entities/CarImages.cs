using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarImages")]
    public class CarImage
    {
        [Key] public int Id { get; set; }
        [Column("ObjectId")] public long ObjectId { get; set; }
        public string? Url { get; set; }
        public string? AltText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Ordinal { get; set; }
        public string? Title { get; set; }
        public CarImageType Type { get; set; }
        public string? ColorCode { get; set; }
        public ImageOfObject ImageOfObject { get; set; }
        
    }
}