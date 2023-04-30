using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities
{
    [Table("Regions")]
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAlias { get; set; }
        public string NameEN { get; set; }
    }
}
