using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarInfoPropertyValue;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;

public class CreateCarInfoPropertyValueCommandHandler : IRequestHandler<CreateCarInfoPropertyValueCommand, List<int>>
{
    private readonly IRepository<Domain.Entities.CarInfoPropertyValue> _carInfoPropertyValueRepository;
    private readonly IMapper _mapper;
    private readonly IModelPropertyValuesService _modelPropertyValuesService;
    private readonly IRepository<Domain.Entities.CarProperty> _carPropertyRepository;
    private readonly IRepository<Domain.Entities.CarPropertyComboboxOption> _carPropertyComboboxOptionRepository;
    public CreateCarInfoPropertyValueCommandHandler(
        IRepository<Domain.Entities.CarInfoPropertyValue> carInfoPropertyValueRepository, 
        IMapper mapper,
        IModelPropertyValuesService modelPropertyValuesService,
        IRepository<Domain.Entities.CarProperty> carPropertyRepository,
        IRepository<Domain.Entities.CarPropertyComboboxOption> carPropertyComboboxOptionRepository)
    {
        _carInfoPropertyValueRepository = carInfoPropertyValueRepository;
        _mapper = mapper;
        _modelPropertyValuesService = modelPropertyValuesService;
        _carPropertyRepository = carPropertyRepository;
        _carPropertyComboboxOptionRepository = carPropertyComboboxOptionRepository;
    }

    public async Task<List<int>> Handle(CreateCarInfoPropertyValueCommand request, CancellationToken cancellationToken)
    {
        var carInfoProValueForCreation = request.CarInfoPropertyListForCreation;
        await Guard.Against.Validate(carInfoProValueForCreation, new CreateCarInfoPropertyValueValidator());

        var carInfoPropertyValueSpec = new CarInfoPropertyValueSpec();
        carInfoPropertyValueSpec.GetByCarInfo(carInfoProValueForCreation.CarInfoId.ToLong());
        carInfoPropertyValueSpec.Select();


        var carInfoProValues = await _carInfoPropertyValueRepository.ListAsync(carInfoPropertyValueSpec, cancellationToken);


        if (carInfoProValues != null && carInfoProValues.Any())
        {
            throw new ConflictException("Specification for CarInfo",
                new { carInfoProValueForCreation.CarInfoId });
            //throw new ApplicationException($"Specification for carInfo ({ carInfoProValueForCreation.CarInfoId }) is existed");
        }

        var carInfoPropertyValueList =
            _mapper.Map<List<Domain.Entities.CarInfoPropertyValue>>(carInfoProValueForCreation
                .CarInfoPropertyValues);

        var carProperties = await _carPropertyRepository.ListAsync();
        var carPropertyComboboxOptions = await _carPropertyComboboxOptionRepository.ListAsync();
        foreach (var value in carInfoPropertyValueList)
        {
            var carProperty = carProperties.FirstOrDefault(p => p.Id == value.CarPropertyId);
            if (carProperty != null)
            {
                switch (carProperty!.Type)
                {
                    case Domain.Entities.Enums.CarPropertyType.Number:
                        value.Value = value.NumberValue > 0 ? value.NumberValue.ToString() : null;
                        break;
                    case Domain.Entities.Enums.CarPropertyType.ComboBox:
                        var comboboxOption = carPropertyComboboxOptions.FirstOrDefault(o => o.Id == value.NumberValue);
                        value.Value = comboboxOption != null ? comboboxOption.Name : value.Value;
                        break;
                }
            }
        }



        await _carInfoPropertyValueRepository.AddRangeAsync(carInfoPropertyValueList);
        await _carInfoPropertyValueRepository.SaveChangesAsync(cancellationToken);

        #region Summary thông số kỹ thuật của model
        await _modelPropertyValuesService.SummaryProperties(request.CarInfoPropertyListForCreation.ModelId, request.UserId);
        #endregion

        return carInfoPropertyValueList.Select(c => c.Id).ToList();
    }
}