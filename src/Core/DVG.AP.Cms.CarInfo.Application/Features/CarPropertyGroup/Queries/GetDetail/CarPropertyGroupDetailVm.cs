using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetDetail;

public class CarPropertyGroupDetailVm
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Ordinal { get; set; }
    public short Status { get; set; }
    public IEnumerable<CarPropertyDetailVm> CarProperties { get; set; } = new List<CarPropertyDetailVm>();
}

public class CarPropertyDetailVm
{
    public int Id { get; set; }
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