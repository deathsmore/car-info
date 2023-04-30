using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices
{
    public class ModelPropertyValuesService : IModelPropertyValuesService
    {
        private readonly IModelPropertyValueRepository _modelPropertyValueRepository;
        private readonly ICarPropertyRepository _carPropertyRepository;
        private readonly ICarInfoPropertyValueRepository _carInfoPropertyValueRepository;
        private readonly IRepository<Domain.Entities.CarPropertyComboboxOption> _carPropertyComboboxOptionRepository;
        public readonly ILogger _logger;
        public ModelPropertyValuesService(
            IModelPropertyValueRepository modelPropertyValueRepository,
            ICarPropertyRepository carPropertyRepository,
            ICarInfoPropertyValueRepository carInfoPropertyValueRepository,
            ILoggerFactory loggerFactory,
            IRepository<Domain.Entities.CarPropertyComboboxOption> carPropertyComboboxOptionRepository)
        {
            _modelPropertyValueRepository = modelPropertyValueRepository;
            _carPropertyRepository = carPropertyRepository;
            _carInfoPropertyValueRepository = carInfoPropertyValueRepository;
            _carPropertyComboboxOptionRepository = carPropertyComboboxOptionRepository;
            _logger = loggerFactory.CreateLogger("ApplicationServices");
        }
        public async Task SummaryProperties(int modelId, int currentUserId)
        {
            try
            {
                var oldModelPropertyValues = await _modelPropertyValueRepository.ListAsync(modelId);
                await _modelPropertyValueRepository.DeleteRangeAsync(oldModelPropertyValues);

                var propertyMainSpec = await _carPropertyRepository.GetModelSpecs();
                if (propertyMainSpec == null || !propertyMainSpec.Any()) return;

                //Lấy tất cả thông số kỹ thuật (đc set là modelSpec) của các carInfo thuộc model hiện tại
                var carInfoModelSpecPropertyValues = await _carInfoPropertyValueRepository.ListCarInfoSpecByModelAsync(modelId, true);
                if (carInfoModelSpecPropertyValues == null || !carInfoModelSpecPropertyValues.Any()) return;

                //Lấy tất cả các combobox options
                var carPropertyComboboxOptions = await _carPropertyComboboxOptionRepository.ListAsync();

                List<ModelPropertyValue> newModelPropertyValues = new List<ModelPropertyValue>();
                foreach (var property in propertyMainSpec)
                {
                    double minValue = 0, maxValue = 0;
                    string? value = null;
                    

                    var propertyValuesById = carInfoModelSpecPropertyValues.Where(pv => pv.CarPropertyId == property.Id).ToList();
                    if (propertyValuesById.Any())
                    {
                        string? unit = property.Unit == Domain.Enums.PropertyUnit.Default ? null : property.Unit.ToString();   
                        switch (property.Type)
                        {
                            case Domain.Entities.Enums.CarPropertyType.TextBox:
                                propertyValuesById.ForEach(x => x.Value = !string.IsNullOrEmpty(x.Value) ? Regex.Replace(x.Value, @"\s+", " ") : null);//Bỏ multiple space
                                var valuesUnique = propertyValuesById.Where(x => !string.IsNullOrEmpty(x.Value))
                                    .GroupBy(x => x.Value?.ToLower()).Select(y => y.First()).Distinct();//Loại bỏ giá trị trùng, lấy giá trị đầu tiên trong tập bị trùng

                                value = valuesUnique != null ? string.Join(", ", valuesUnique.Select(x => $"{x.Value}{(unit == null ? null : $" {unit}")}").Distinct()) : null;
                                break;
                            case Domain.Entities.Enums.CarPropertyType.Number:
                                minValue = propertyValuesById!.Min(pv => pv.NumberValue);
                                maxValue = propertyValuesById!.Max(pv => pv.NumberValue);
                                value = string.Join(", ", propertyValuesById.Where(x => x.NumberValue > 0).OrderBy(x => x.NumberValue).Select(x => $"{x.NumberValue}{(unit == null ? null: $" {unit}")}").Distinct());
                                break;
                            case Domain.Entities.Enums.CarPropertyType.ComboBox:
                                var comboboxOptionIds = propertyValuesById!.Select(x => x.NumberValue).Distinct();//Lấy ra danh sách các option được chọn (để tổng hợp dựa theo short name nếu có)
                                if(comboboxOptionIds == null || !comboboxOptionIds.Any())
                                {
                                    continue;
                                }

                                var selectedComboboxOptions = carPropertyComboboxOptions.Where(x => comboboxOptionIds.Contains(x.Id)).OrderBy(x => x.Name);
                                if(selectedComboboxOptions != null && selectedComboboxOptions.Any())
                                {
                                    value = string.Join(", ", selectedComboboxOptions.Select(x => $"{(!string.IsNullOrEmpty(x.ShortName) ? x.ShortName : x.Name)}{(unit == null ? null : $" {unit}")}").Distinct());
                                }
                                else
                                {
                                    value = string.Join(", ", propertyValuesById.Where(x => !string.IsNullOrEmpty(x.Value)).Select(x => $"{x.Value}{(unit == null ? null : $" {unit}")}").Distinct());
                                }
                                
                                break;
                        }
                    }

                    var modelPropertyValue = new ModelPropertyValue()
                    {
                        CarPropertyId = property.Id,
                        CreatedDate = DateTime.Now,
                        CreatedBy = currentUserId,
                        ModelId = modelId,
                        Value = value,
                        MinValue = minValue,    
                        MaxValue = maxValue
                    };
                    newModelPropertyValues.Add(modelPropertyValue);
                }

                await _modelPropertyValueRepository.AddRangeAsync(newModelPropertyValues);
                await _modelPropertyValueRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            }
            
        }
    }
}
