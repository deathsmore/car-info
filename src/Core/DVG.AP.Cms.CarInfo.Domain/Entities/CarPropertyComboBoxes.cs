using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarPropertyComboBoxes")]
    public class CarPropertyComboBox
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }

        public short Status { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public IEnumerable<CarPropertyComboboxOption> CarPropertyComboboxOptions { get; set; } =
            new List<CarPropertyComboboxOption>();
    }
}