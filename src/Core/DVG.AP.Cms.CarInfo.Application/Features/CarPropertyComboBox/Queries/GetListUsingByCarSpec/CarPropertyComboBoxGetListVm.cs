namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetListUsingByCarSpec;

public class CarPropertyComboBoxGetListUsedVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public short Status { get; set; }


    public IEnumerable<CarPropertyComboboxOptionGetListUsedVm> CarPropertyComboboxOptions =>
        _carPropertyComboboxOptions.AsEnumerable();

    private readonly List<CarPropertyComboboxOptionGetListUsedVm> _carPropertyComboboxOptions = new();
}

public class CarPropertyComboboxOptionGetListUsedVm
{
    public int Id { get; set; }
    public int CarPropertyComboboxId { get; set; }
    public string ShortName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}