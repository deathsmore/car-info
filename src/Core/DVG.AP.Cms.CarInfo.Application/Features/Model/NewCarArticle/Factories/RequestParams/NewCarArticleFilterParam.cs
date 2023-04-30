using System.ComponentModel;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Infrastructures.Base;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.RequestParams;

public class NewCarArticleFilterParam : PagingParam
{
    public int BrandId { get; set; }

    // Truyển List Id lên thì không lấy NewCarArticle có Id trong list này.
    public string[]? ExcludeIds { get; set; }


    public int ModelId { get; set; }

    public int VariantId { get; set; }

    [DefaultValue(NewCarArticleType.Default)]
    public NewCarArticleType Type { get; set; }

    public NewCarArticleStatus Status { get; set; }
    public ContentFormatTag ContentFormatTag { get; set; }
    public int ContentAngleTag { get; set; }
    public SourceTag SourceTag { get; set; }
    public int CreatedBy { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreatedDateTo { get; set; }
}