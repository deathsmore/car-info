namespace DVG.AP.Cms.CarInfo.Application.Features.Dtos;

public class CarStructureDto
{
    public int BrandId { get; set; }
    public string? BrandName { get; set; }
    public int ModelId { get; set; }
    public string? ModelName { get; set; }
    public  int VariantId { get; set; }
    public string? VariantName { get; set; }
}