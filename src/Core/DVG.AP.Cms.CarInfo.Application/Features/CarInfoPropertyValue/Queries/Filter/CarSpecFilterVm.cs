namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;

public class CarSpecFilterVm
{
    public CarSpecFilterVm(
        long carInfoId, string brandName, string? modelName, string? carInfoName, int year,
        int brandId, int modelId)
    {
        CarInfoId = carInfoId.ToString();
        BrandName = brandName;
        ModelName = modelName;
        CarInfoName = carInfoName;
        Year = year;
        BrandId = brandId;
        ModelId = modelId;
    }

    public CarSpecFilterVm()
    {
    }

    public string CarInfoId { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }

    public string BrandName { get; set; }
    public string? ModelName { get; set; }
    public string? CarInfoName { get; set; }
    public int Year { get; set; }
}