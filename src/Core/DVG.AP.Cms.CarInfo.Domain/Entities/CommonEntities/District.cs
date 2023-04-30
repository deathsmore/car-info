using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities
{
    [Table("Districts")]
    public class District
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string NameAlias { get; set; }
        public string NameEN { get; set; }
        public int CityId { get; set; }
        public int RegionId { get; set; }
        public virtual City City { get; set; }
    }
}
