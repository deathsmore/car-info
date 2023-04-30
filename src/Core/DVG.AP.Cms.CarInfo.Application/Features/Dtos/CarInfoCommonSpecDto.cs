using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Dtos
{
    public class CarInfoCommonSpecDto
    {
        public int? BodyTypeId { get; set; }
        public string? BodyTypeName { get; set; }
        public string? BodyTypeShortName { get; set; }
        public int? TransmissionId { get; set; }
        public string? TransmissionName { get; set; }
        public string? TransmissionShortName { get; set; }
        public int? FuelTypeId { get; set; }
        public string? FuelTypeName { get; set; }
        public string? FuelTypeShortName { get; set; }

        public string? Engine { get; set; }
        public ActiveStatus Status { get; set; }
        public EngineCapacityDto EngineCapacity {
            get
            {
                if (string.IsNullOrEmpty(Engine))
                {
                    return null;
                }
                else
                {
                    try
                    {
                        var result = new EngineCapacityDto();
                        var engineTrim = Regex.Replace(Engine.Trim(), @"\s+", " ");
                        var split = engineTrim.Split(' ');
                        if(split.Length > 0)
                        {
                            double capacity;
                            Double.TryParse(split[0], out capacity);
                            result.EngineCapacity = capacity;
                        }    

                        if (split.Length > 1)
                        {
                            var unit = string.IsNullOrEmpty(split[1]) ? EngineCapacityUnit.l.ToString() : split[1];
                            result.Unit = unit;
                        }    
                        return result;
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                    
                }
            }
        }
        public short NumOfSeat { get; set; }
        public double MaxPower { get; set; }
        public double MaxTorque { get; set; }
        public short NumOfDoor { get; set; }

        public static Expression<Func<Domain.Entities.CarInfo, CarInfoCommonSpecDto>> MapPropertyFunc =
        c => new CarInfoCommonSpecDto()
        {
            BodyTypeId = c.BodyTypeId,
            BodyTypeName = c.BodyType == null ? null : c.BodyType!.Name,
            BodyTypeShortName = c.BodyType == null ? null : c.BodyType!.ShortName,
            Engine = c.Engine,
            TransmissionId = c.TransmissionId,
            TransmissionName = c.Transmission == null ? null : c.Transmission!.Name,
            TransmissionShortName = c.Transmission == null ? null : c.Transmission!.ShortName,
            FuelTypeId = c.FuelTypeId,
            FuelTypeName = c.FuelType == null ? null : c.FuelType!.Name,
            FuelTypeShortName = c.FuelType == null ? null : c.FuelType!.ShortName,
            NumOfSeat = c.NumOfSeat,
            MaxPower = c.MaxPower,
            MaxTorque = c.MaxTorque,
            NumOfDoor = c.NumOfDoor
        };
    }

    public class EngineCapacityDto
    {
        public double EngineCapacity { get; set; }
        public string Unit { get; set; }
    }
}
