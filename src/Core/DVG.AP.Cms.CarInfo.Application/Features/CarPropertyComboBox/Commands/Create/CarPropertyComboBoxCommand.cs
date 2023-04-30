using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Create;

public class CarPropertyComboBoxCreateCommand : IRequest<int>
{
    public CarPropertyComboBoxCreateCommand(int userId, CarPropertyComboBoxForCreation comboBoxForCreation)
    {
        ComboBoxForCreation = comboBoxForCreation;
        ComboBoxForCreation.Init(userId);
    }

    public CarPropertyComboBoxForCreation ComboBoxForCreation { get; }
}

public class CarPropertyComboBoxForCreation
{
    public void Init(int userId)
    {
        CreatedBy = userId;
        CreatedDate = DateTime.Now;
        ModifiedDate = DateTime.Now;
        ModifiedBy = userId;
    }

    public string? Name { get; set; }

    public short Status { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public int ModifiedBy { get; set; }

    public IEnumerable<CarPropertyComboBoxOptionForCreation> CarPropertyComboboxOptions { get; set; } =
        new List<CarPropertyComboBoxOptionForCreation>();

}

public class CarPropertyComboBoxOptionForCreation
{
    public int CarPropertyComboboxId { get; set; }
    public string? ShortName { get; set; }
    public string? Name { get; set; }
}