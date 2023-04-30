using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("Models")]
    public class Model
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? Slug { get; set; }
        public int BrandId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Ordinal { get; set; }
        public bool? IsShowListSearch { get; set; }
        public ActiveStatus Status { get; set; }
        public int SegmentId { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<Variant>? Variants { get; set; }
        public ModelPropertySummary? ModelPropertySummary { get; set; }
    }
}