namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.GetDetail;

public class CarInfoDetailIncludePropertyValueVm
{
   
    public int Year { get; set; }
    
    public string CarInfoId { get; set; } = string.Empty;
    public  int VariantId { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }


    public IEnumerable<CarInfoPropertyValueDetailVm> CarInfoPropertyValueDetailVms { get; set; } =
        new List<CarInfoPropertyValueDetailVm>();
   


   
}
public class CarInfoPropertyValueDetailVm
{
       

    public string? Value { get; set; }
    public double NumberValue { get; set; }
    public DateTime? DateValue { get; set; }
    public int[]? ListValue { get; set; }
    public int CarPropertyId { get; set; }
    public int CarPropertyComboBoxId { get; set; }
    public string? CarInfoId { get; set; }
    public int Year { get; set; }
}