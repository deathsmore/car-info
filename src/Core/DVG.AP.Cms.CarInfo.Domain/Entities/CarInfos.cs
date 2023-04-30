using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("CarInfos")]
    public class CarInfo : EntityBase<long>
    {
        //public int ModelId { get; set; }
        public int VariantId { get; set; }
        public int? BodyTypeId { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public string? Url { get; set; }
        public int Year { get; set; }
        public string? Engine { get; set; }
        public int? FuelTypeId { get; set; }
        public int? TransmissionId { get; set; }
        public short NumOfSeat { get; set; }
        public double MaxPower { get; set; }
        public double MaxTorque { get; set; }
        public short NumOfDoor { get; set; }
        public DateTime? LaunchedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    public ActiveStatus Status { get; set; }
        public bool IsDiscontinued { get; set; }
        public int MakeOverId { get; set; }

        public bool IsLatest { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        //Relations
        //public Model Model { get; set; }

        public Variant Variant { get; set; }


        public IEnumerable<CarPrice> Prices => _prices.AsEnumerable();
        private readonly List<CarPrice> _prices = new();

        //public IEnumerable<CarImage> Images => _images.AsEnumerable();
        //private readonly List<CarImage> _images = new();
        public BodyType? BodyType { get; set; }
        public FuelType? FuelType { get; set; }
        public Transmission? Transmission { get; set; }

        public IEnumerable<CarInfoPropertyValue> CarInfoPropertyValues => _carInfoPropertyValues.AsEnumerable();
        public virtual IEnumerable<CarImage>? Images { get; set; }

        private readonly List<CarInfoPropertyValue> _carInfoPropertyValues = new();
    }
}
