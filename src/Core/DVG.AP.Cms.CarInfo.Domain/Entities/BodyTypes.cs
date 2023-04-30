using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("BodyTypes")]
    public class BodyType
    {
        [Key] public int Id { get; set; }

        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public short Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public ICollection<DVG.AP.Cms.CarInfo.Domain.Entities.CarInfo> CarInfos { get; set; }
    }
}