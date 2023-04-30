using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("ModelPropertyValues")]
    public class ModelPropertyValue
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int CarPropertyId { get; set; }
        public string? Value { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
