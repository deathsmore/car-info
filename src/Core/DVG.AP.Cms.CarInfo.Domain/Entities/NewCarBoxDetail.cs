using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
   [Table("NewCarBoxDetails")]
   public class NewCarBoxDetail
    {
        [Key]
        public int Id { get; set; }
        public int NewCarBoxId { get; set; }
        public long NewCarArticleId { get; set; }
        public short Ordinal { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// ObjectId = BrandId | ModelId | CarInfoId. dependence ObjectType field
        /// </summary>
        public long ObjectId { get; set; }
        /// <summary>
        /// ObjectType = 1: Brand, 2: Model, 3: Variant
        /// </summary>
        public NewCarArticleType ObjectType { get; set; }
    }
}
