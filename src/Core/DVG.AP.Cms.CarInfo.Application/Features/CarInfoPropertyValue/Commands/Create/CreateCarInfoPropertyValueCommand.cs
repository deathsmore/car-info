using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Create;

public class CreateCarInfoPropertyValueCommand : IRequest<List<int>>
{
    public CreateCarInfoPropertyValueCommand(int userId, CarInfoPropertyListForCreation carInfoPropertyListForCreation)
    {
        CarInfoPropertyListForCreation = carInfoPropertyListForCreation;
        carInfoPropertyListForCreation.CarInfoPropertyValues.ForEach(
            cp => cp.Init(carInfoId: carInfoPropertyListForCreation.CarInfoId,userId: userId)
        );
        UserId = userId;
    }


    public CarInfoPropertyListForCreation CarInfoPropertyListForCreation { get; set; }
    [NotMapped]
    public int UserId { get; set; }
}

public class CarInfoPropertyListForCreation
{
    public string CarInfoId { get; set; }
    public int ModelId { get; set; }

    public List<CarInfoPropertyForCreation> CarInfoPropertyValues { get; set; } = new();
}

public class CarInfoPropertyForCreation
{
    public void Init(string carInfoId, int userId)
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