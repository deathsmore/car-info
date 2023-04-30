using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Dtos;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices
{
    public class ModelPropertySummariesService : IModelPropertySummariesService
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IModelPropertySummaryRepository _modelPropertySummaryRepository;
        private readonly IConfiguration Configuration;
        public ModelPropertySummariesService(
            ICarInfoRepository carInfoRepository,
            IModelPropertySummaryRepository modelPropertySummaryRepository,
            IConfiguration configuration
        )
        {
            _carInfoRepository = carInfoRepository;
            _modelPropertySummaryRepository = modelPropertySummaryRepository;
            Configuration = configuration;
        }
        public async Task SyncSummarySpec(int modelId)
        {
            try
            {
                bool isLatestOnly = Configuration.GetValue<bool>("AppSettings:ModelSpecSummaryFromLastestCarInfoOnly");
                List<Expression<Func<Domain.Entities.CarInfo, bool>>> conditions = new List<Expression<Func<Domain.Entities.CarInfo, bool>>>();
                conditions.Add(c => c.Variant.ModelId.Equals(modelId) && c.Status == ActiveStatus.Active);
                if (isLatestOnly)
                {
                    conditions.Add(c => c.IsLatest);
                }

                var carInfos = await _carInfoRepository.ListAsync(0, null,
                                    CarInfoCommonSpecDto.MapPropertyFunc,
                                    conditions.ToArray()
                                );
                var modelPropertySummary = await _modelPropertySummaryRepository.GetByIdAsync(modelId);

                bool isCreating = false;
                if (carInfos is not null && carInfos.Any())
                {
                    if (modelPropertySummary == null)
                    {
                        isCreating = true;
                        modelPropertySummary = new Domain.Entities.ModelPropertySummary()
                        {
                            ModelId = modelId
                        };
                    }

                    modelPropertySummary.BodyType = string.Join(", ", 
                        carInfos.Select(x => !string.IsNullOrEmpty(x.BodyTypeShortName) ? x.BodyTypeShortName : x.BodyTypeName).
                        Where(x=> !string.IsNullOrEmpty(x)).Distinct().ToList());

                    modelPropertySummary.Transmission = string.Join(", ",
                        carInfos.Select(x => !string.IsNullOrEmpty(x.TransmissionShortName) ? x.TransmissionShortName : x.TransmissionName).
                        Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList());

                    modelPropertySummary.FuelType = string.Join(", ",
                        carInfos.Select(x => !string.IsNullOrEmpty(x.FuelTypeShortName) ? x.FuelTypeShortName : x.FuelTypeName).
                        Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList());

                    //Engine
                    List<string> engineRangeByUnit = new List<string>();
                    List<EngineCapacityDto> availableEngines = carInfos.Select(x => x.EngineCapacity).Where(x => x != null && x.EngineCapacity > 0).ToList();
                    var availableEnginesGroupByUnit = availableEngines.GroupBy(x => x.Unit);
                    foreach(var unit in availableEnginesGroupByUnit)
                    {
                        double? engineCapacityMin = unit.Any() ? unit.Min(x=> x.EngineCapacity) : null;
                        double? engineCapacityMax = unit.Any() ? unit.Max(x => x.EngineCapacity) : null;

                        engineRangeByUnit.Add(engineCapacityMin == engineCapacityMax ? $"{engineCapacityMax} {unit.Key}" : $"{engineCapacityMin} - {engineCapacityMax} {unit.Key}");
                    }
                    modelPropertySummary.Engine = engineRangeByUnit.Any() ? string.Join(", ", engineRangeByUnit) : null;

                    //MaxPower
                    List<double> availableMaxPowers = carInfos.Select(x => x.MaxPower).Where(x => x > 0).ToList();
                    double? maxPowerMin = availableMaxPowers != null && availableMaxPowers.Any() ? availableMaxPowers.Min() : null;
                    double? maxPowerMax = availableMaxPowers != null && availableMaxPowers.Any() ? availableMaxPowers.Max() : null;
                    modelPropertySummary.MaxPower = maxPowerMin == maxPowerMax ? (maxPowerMax != null ? $"{maxPowerMax} hp" : null ) : $"{maxPowerMin} - {maxPowerMax} hp";//T-TEMP, hardcode unit

                    //MaxTorque
                    List<double> availableMaxTorques = carInfos.Select(x => x.MaxTorque).Where(x => x > 0).ToList();
                    double? maxTorqueMin = availableMaxTorques != null && availableMaxTorques.Any() ? availableMaxTorques.Min() : null;
                    double? maxTorqueMax = availableMaxTorques != null && availableMaxTorques.Any() ? availableMaxTorques.Max() : null;
                    modelPropertySummary.MaxTorque = maxTorqueMin == maxTorqueMax ? (maxTorqueMax != null ? $"{maxTorqueMax} nm" : null) : $"{maxTorqueMin} - {maxTorqueMax} nm";//T-TEMP, hardcode unit

                    //NumOfSeat
                    List<short> availableNumOfSeats = carInfos.Select(x => x.NumOfSeat).Where(x => x > 0).ToList();
                    short? numOfSeatMin = availableNumOfSeats != null && availableNumOfSeats.Any() ? availableNumOfSeats.Min() : null;
                    short? numOfSeatMax = availableNumOfSeats != null && availableNumOfSeats.Any() ? availableNumOfSeats.Max() : null;
                    modelPropertySummary.NumOfSeat = numOfSeatMin == numOfSeatMax ? $"{numOfSeatMax}" : $"{numOfSeatMin} - {numOfSeatMax}";

                    //NumOfDoor
                    List<short> availableNumOfDoors = carInfos.Select(x => x.NumOfDoor).Where(x => x > 0).ToList();
                    short? numOfDoorMin = availableNumOfDoors != null && availableNumOfDoors.Any() ? availableNumOfDoors.Min() : null;
                    short? numOfDoorMax = availableNumOfDoors != null && availableNumOfDoors.Any() ? availableNumOfDoors.Max() : null;
                    modelPropertySummary.NumOfDoor = numOfDoorMin == numOfDoorMax ? $"{numOfDoorMax}" : $"{numOfDoorMin} - {numOfDoorMax}";
                    
                    //Liet ke
                    //modelPropertySummary.BodyType = string.Join(", ", carInfos.Where(x => !string.IsNullOrEmpty(x.BodyTypeName)).Select(x => x.BodyTypeName).Distinct().ToList());
                    //modelPropertySummary.Engine = string.Join(", ", carInfos.Where(x => !string.IsNullOrEmpty(x.Engine)).Select(x => x.Engine).Distinct().ToList());
                    //modelPropertySummary.Transmission = string.Join(", ", carInfos.Where(x => !string.IsNullOrEmpty(x.TransmissionName)).Select(x => x.TransmissionName).Distinct().ToList());
                    //modelPropertySummary.FuelType = string.Join(", ", carInfos.Where(x => !string.IsNullOrEmpty(x.FuelTypeName)).Select(x => x.FuelTypeName).Distinct().ToList());
                    //modelPropertySummary.NumOfSeat = string.Join(", ", carInfos.Where(x => x.NumOfSeat > 0).OrderBy(x => x.NumOfSeat).Select(x => x.NumOfSeat.ToString()).Distinct().ToList());
                    //modelPropertySummary.MaxPower = string.Join(", ", carInfos.Where(x => x.MaxPower > 0).OrderBy(x => x.MaxPower).Select(x => x.MaxPower.ToString()).Distinct().ToList());
                    //modelPropertySummary.MaxTorque = string.Join(", ", carInfos.Where(x => x.MaxTorque > 0).OrderBy(x => x.MaxTorque).Select(x => x.MaxTorque.ToString()).Distinct().ToList());
                    //modelPropertySummary.NumOfDoor = string.Join(", ", carInfos.Where(x => x.NumOfDoor > 0).OrderBy(x => x.NumOfDoor).Select(x => x.NumOfDoor.ToString()).Distinct().ToList());

                    await (isCreating ? _modelPropertySummaryRepository.AddAsync(modelPropertySummary) : _modelPropertySummaryRepository.UpdateAsync(modelPropertySummary));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
