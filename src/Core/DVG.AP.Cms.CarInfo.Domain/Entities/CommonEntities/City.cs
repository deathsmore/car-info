using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities
{
    [Table("Cities")]
    public class City
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int RegionId { get; set; }
        public string NameEnglish { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int CreatedUser { get; set; }
        public int ModifiedUser { get; set; }
        public short OrderName { get; set; }
        public virtual Region Region { get; set; }
    }
}
