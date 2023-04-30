using DVG.AP.Cms.CarInfo.Application.Contracts.Extensions;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert
{
    public class CarInfoInsertCommand : IRequest<long>
    {
        public CarInfoInsertCommand(CarInfoInsert carInfoInsert, int userId)
        {
            CarInfoInsert = carInfoInsert;
            CarInfoInsert.Init(userId);
        }

        public CarInfoInsert CarInfoInsert { get; set; }
    }

    public class CarInfoInsert
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userId;
            Id = DateTime.Now.ToUnixTimeSeconds();// new long().CreateUid();
        }

        public long Id { get; private set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int VariantId { get; set; }
        public int BodyTypeId { get; set; }
        public string? Name { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public short TransmissionId { get; set; }
        public short FuelTypeId { get; set; }
        public short NumOfSeat { get; set; }
        public double MaxPower { get; set; }
        public double MaxTorque { get; set; }
        public short NumOfDoor { get; set; }
        public DateTime? LaunchedDate { get; set; }
        public int CreatedBy { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public ActiveStatus Status { get; set; }
        public bool IsDiscontinued { get; set; }
        public bool HaveNoVariant { get; set; }
        public List<CarPriceForCreation> Prices { get; set; }
        [JsonIgnore]
        public double MinPrice
        {
            get
            {
                return Prices != null && Prices.Any() ? Prices.Min(x => x.Price) : 0D;
            }
        }
        [JsonIgnore]
        public double MaxPrice
        {
            get
            {
                return Prices != null && Prices.Any() ? Prices.Max(x => x.Price) : 0D;
            }
        }
        public List<CarImageForCreation> Images { get; set; }
    }
}