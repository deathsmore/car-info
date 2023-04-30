using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;

public class NewCarArticleFilterDto
{
    public string? Id { get; set; }
    public int? BrandId { get; set; }
    public string? BrandName { get; set; }
    public int? ModelId { get; set; }
    public string? ModelName { get; set; }
    public int? VariantId { get; set; }
    public string? VariantName { get; set; }
    public string? Url { get; set; }
    public string? Title { get; set; }
    public NewCarArticleStatus Status { get; set; }
    public NewCarArticleType Type { get; set; }
    
    public DateTime? PublishedDate { get; set; }
    public int CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTime ModifiedDate { get; set; }
}