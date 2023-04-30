using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("ModelPropertySummaries")]
    public class ModelPropertySummary
    {
        [Key, ForeignKey("Models")]
        public int ModelId { get; set; }
        public string? BodyType { get; set; }
        public string? Engine { get; set; }
        public string? Transmission { get; set; }
        public string? FuelType { get; set; }
        public string? NumOfSeat { get; set; }
        public string? MaxPower { get; set; }
        public string? MaxTorque { get; set; }
        public string? NumOfDoor { get; set; }
    }
}
