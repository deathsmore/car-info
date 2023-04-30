namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetDetail;

public class CarPropertyComboBoxDetail
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public short Status { get; set; }

    public IEnumerable<CarPropertyComboBoxOptionDetailVm> CarPropertyComboboxOptions =>
        _carPropertyComboboxOptions.AsEnumerable();

    private readonly List<CarPropertyComboBoxOptionDetailVm> _carPropertyComboboxOptions = new();
}

public class CarPropertyComboBoxOptionDetailVm
{
    public int Id { get; set; }
    public int CarPropertyComboboxId { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}