using DVG.AP.Cms.CarInfo.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    public class NewCarSEOInfos
    {
        [Key]
        public long NewCarArticleId { get; set; }
        public short Type { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public int ContentAngleTag { get; set; }
        public ContentFormatTag ContentFormatTag { get; set; }
        public SourceTag SourceTag { get; set; }
        public string? SEOKeyword { get; set; }
        public string? TitleSeo { get; set; }
    }
}
