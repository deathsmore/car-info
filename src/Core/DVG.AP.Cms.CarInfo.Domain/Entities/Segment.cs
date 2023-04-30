using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("Segments")]
    public class Segment
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
