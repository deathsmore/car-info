using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Update;

public class CarPropertyComboBoxUpdateCommand : IRequest<Unit>
{
    public CarPropertyComboBoxUpdateCommand(int userId, CarPropertyComboBoxForUpdate comboBoxForCreation, int id)
    {
        ComboBoxForUpdate = comboBoxForCreation;
        Id = id;
        ComboBoxForUpdate.Init(userId);
    }

    public int Id { get; }
    public CarPropertyComboBoxForUpdate ComboBoxForUpdate { get; }
}

public class CarPropertyComboBoxForUpdate
{
    public void Init(int userId)
    {
        ModifiedDate = DateTime.Now;
        ModifiedBy = userId;
    }

    public string? Name { get; set; }

    public short Status { get; set; }
    public DateTime ModifiedDate { get; set; }

    public int ModifiedBy { get; set; }

    public IEnumerable<CarPropertyComboBoxOptionForUpdate> CarPropertyComboboxOptions { get; set; } =
        new List<CarPropertyComboBoxOptionForUpdate>();
}

public class CarPropertyComboBoxOptionForUpdate
{
    public int CarPropertyComboboxId { get; set; }
    public string? ShortName { get; set; }
    public string? Name { get; set; }
}