namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models
{
    public class SEOInfoUpdate
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public int? ContentAngleTag { get; set; }
        public int? ContentFormatTag { get; set; }
        public int? SourceTag { get; set; }
        public string? OgTitle { get; set; }
        public string? OgImage { get; set; }
        public string? OgDescription { get; set; }
        public string? TitleLink { get; set; }
        public string? BackLink { get; set; }
        public string? TitleSeo { get; set; }
        public string? SEOKeyword { get; set; }
    }
}
