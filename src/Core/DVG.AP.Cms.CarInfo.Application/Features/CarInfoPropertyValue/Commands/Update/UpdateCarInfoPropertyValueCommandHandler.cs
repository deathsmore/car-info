using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;

public class UpdateCarInfoPropertyValueCommandHandler : IRequestHandler<UpdateCarInfoPropertyValueCommand>
{
    private readonly ICarInfoPropertyValueRepository _carInfoPropertyValueRepository;
    private readonly IMapper _mapper;
    private readonly IModelPropertyValuesService _modelPropertyValuesService;
    private readonly IRepository<Domain.Entities.CarProperty> _carPropertyRepository;
    private readonly IRepository<Domain.Entities.CarPropertyComboboxOption> _carPropertyComboboxOptionRepository;

    public UpdateCarInfoPropertyValueCommandHandler(
        ICarInfoPropertyValueRepository carInfoPropertyValueRepository,
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

    public async Task<Unit> Handle(UpdateCarInfoPropertyValueCommand request, CancellationToken cancellationToken)
    {
        var carInfoProValueForUpdate = request.CarInfoPropertyListForUpdate;
        await Guard.Against.Validate(carInfoProValueForUpdate, new UpdateCarInfoPropertyValueValidator());
        var carInfoProValues =
            await _carInfoPropertyValueRepository.ListAsync(carInfoProValueForUpdate.CarInfoId.ToLong());
        if (!carInfoProValues.Any())
        {
            throw new NotFoundException(nameof(Domain.Entities.CarInfoPropertyValue),
                new { carInfoProValueForUpdate.CarInfoId });
        }

// delete all car Spec
        await _carInfoPropertyValueRepository.DeleteRangeAsync(carInfoProValues, cancellationToken);


        var carInfoPropertyValueList =
            _mapper.Map<List<Domain.Entities.CarInfoPropertyValue>>(carInfoProValueForUpdate
                .CarInfoPropertyValues);

        var carProperties = await _carPropertyRepository.ListAsync();
        var carPropertyComboboxOptions = await _carPropertyComboboxOptionRepository.ListAsync();
        foreach (var value in carInfoPropertyValueList)
        {
            var carProperty = carProperties.FirstOrDefault(p => p.Id == value.CarPropertyId);
            if(carProperty != null)
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
        await _modelPropertyValuesService.SummaryProperties(carInfoProValueForUpdate.ModelId, request.UserId);
        #endregion
        return Unit.Value;
    }
}