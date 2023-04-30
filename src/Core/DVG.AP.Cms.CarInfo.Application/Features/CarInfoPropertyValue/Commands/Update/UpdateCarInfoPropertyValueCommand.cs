using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;

public class UpdateCarInfoPropertyValueCommand : IRequest<Unit>
{
    public UpdateCarInfoPropertyValueCommand(int userId, CarInfoPropertyListForUpdate carInfoPropertyListForCreation)
    {
        CarInfoPropertyListForUpdate = carInfoPropertyListForCreation;
        carInfoPropertyListForCreation.CarInfoPropertyValues.ForEach(
            cp => cp.Init(carInfoId: carInfoPropertyListForCreation.CarInfoId, userId: userId)
        );
        UserId = userId;
    }


    public CarInfoPropertyListForUpdate CarInfoPropertyListForUpdate{ get; set; }
    [NotMapped]
    public int UserId { get; set; }
}

public class CarInfoPropertyListForUpdate
{
    public string? CarInfoId { get; set; }
    public int ModelId { get; set; }

    public List<CarInfoPropertyForUpdate> CarInfoPropertyValues { get; set; } = new();
}

public class CarInfoPropertyForUpdate
{
    public void Init(string? carInfoId, int userId)
    {
        CarInfoId = carInfoId.ToLong();
        CreatedBy = userId;
        LastModifiedBy = userId;
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
    }

    public string? Value { get; set; }
    public double NumberValue { get; set; }
    public DateTime? DateValue { get; set; }
    public int[]? ListValue { get; set; }
    public int CarPropertyId { get; set; }
    public int CarPropertyComboBoxId { get; set; }
    public long CarInfoId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastModifiedDate { get; private set; }
    public int CreatedBy { get; private set; }
    public int LastModifiedBy { get; private set; }
}