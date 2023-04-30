using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Create;

public class CreateCarPropertyGroupCommand : IRequest<int>
{
    public CreateCarPropertyGroupCommand(int userId, CarPropertyGroupForCreation carPropertyGroupForCreation)
    {
        CarPropertyGroupForCreation = carPropertyGroupForCreation;
        CarPropertyGroupForCreation.Init(userId);
    }

    public CarPropertyGroupForCreation CarPropertyGroupForCreation { get; }
}

public class CarPropertyGroupForCreation
{
    public void Init(int userId)
    {
        CreatedDate = DateTime.Now;
        LastModifiedDate = DateTime.Now;
        CreatedBy = userId;
        LastModifiedBy = userId;
    }

    public string? Name { get; set; }
    public short Status { get; set; }
    public int Ordinal { get; set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastModifiedDate { get; private set; }
    public int CreatedBy { get; private set; }
    public int LastModifiedBy { get; private set; }
    public IEnumerable<CarPropertyForCreation> CarProperties { get; set; } = new List<CarPropertyForCreation>();
}

public class CarPropertyForCreation
{
    public int CarPropertyGroupId { get; set; }
    public int CarPropertyComboBoxId { get; set; }
    public int MaxLength { get; set; }
    public int Ordinal { get; set; }
    public CarPropertyType Type { get; set; }
    public short Status { get; set; }
    public string? Name { get; set; }
    public string? DefaultValue { get; set; }
    public int Unit { get; set; }
    public bool IsRequired { get; set; }
    public bool IsMultiChoice { get; set; }
    public bool IsModelSpec { get; set; }
    public bool IsCrawled { get; set; }
}