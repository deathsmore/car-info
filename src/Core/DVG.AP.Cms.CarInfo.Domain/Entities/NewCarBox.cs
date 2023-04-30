using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("NewCarBoxes")]
   public class NewCarBox
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public short NumberDisplay { get; set; }
        public short Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public NewCarArticleType NewCarType { get; set; }
    }
}
