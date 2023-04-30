// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("Transmissions")]
    public class Transmission
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public short Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
    }

  
}
