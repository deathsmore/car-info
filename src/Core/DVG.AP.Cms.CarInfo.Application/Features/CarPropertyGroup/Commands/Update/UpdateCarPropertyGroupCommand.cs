using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Update;

public class UpdateCarPropertyGroupCommand : IRequest<Unit>
{
    public UpdateCarPropertyGroupCommand(int id, int userId, CarPropertyGroupForUpdate carPropertyGroupForUpdate)
    {
        CarPropertyGroupForUpdate = carPropertyGroupForUpdate;
        CarPropertyGroupForUpdate.Init(userId: userId, id: id);
    }

    public CarPropertyGroupForUpdate CarPropertyGroupForUpdate { get; set; }
}

public class CarPropertyGroupForUpdate
{
    public void Init(int userId, int id)
    {
        Id = id;
        LastModifiedDate = DateTime.Now;
        LastModifiedBy = userId;
    }

    public int Id { get; private set; }
    public string? Name { get; set; }
    public short Status { get; set; }
    public int Ordinal { get; set; }

    public DateTime LastModifiedDate { get; private set; }

    public int LastModifiedBy { get; private set; }

    public IEnumerable<Create.CarPropertyForCreation> CarProperties { get; set; } =
        new List<Create.CarPropertyForCreation>();
}

public class CarPropertyForUpdate
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