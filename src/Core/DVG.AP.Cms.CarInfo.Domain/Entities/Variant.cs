using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("Variants")]
    public class Variant
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsVirtual { get; set; }
        public ActiveStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual Model Model { get; set; }
    }
}
