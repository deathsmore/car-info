using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Dtos;

public class NewCarArticleDto
{
    // Id = nc.Id,
    // ObjectId = nc.ObjectId,
    // Url = nc.Url,
    // Status = nc.Status

    public long Id { get; set; }
    public long CarInfoId { get; set; }
    public long ModelId { get; set; }

    public long BrandId { get; set; }
    public long ObjectId { get; set; }
    public string? Url { get; set; }
    public string? Title { get; set; }
    public NewCarArticleType Type { get; }

    public NewCarArticleStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public NewCarArticleDto()
    {
    }

    public NewCarArticleDto(DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle newCarArticle) : this()
    {
        Id = newCarArticle.Id;
        Url = newCarArticle.Url;
        Status = newCarArticle.Status;
        Type = newCarArticle.Type;
        Title = newCarArticle.Title;
        CreatedDate = newCarArticle.CreatedDate;
        ObjectId = newCarArticle.ObjectId;
    }
}