namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create.Models
{
    public class NewCarSEOInfosForUpdate
    {
        public long NewCarArticleId { get; set; }
        public short Type { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public int ContentAngleTag { get; set; }
        public int ContentFormatTag { get; set; }
        public int SourceTag { get; set; }
        public string? SEOKeyword { get; set; }
        public string? TitleSeo { get; set; }
    }
}
