using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities
{
    [Table("Urls")]
    public class Url
    {
        [Key]
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
