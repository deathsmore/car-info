// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Domain.Entities.Enums
{
    [Table("CurrencyUnit")]
    public class CurrencyUnit
    {
        [Key] public short Id { get; set; }
        public string Name { get; set; }
        public string NameAlias { get; set; }
    }
}