using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Domain.Entities
{
    [Table("SEOInfos")]
    public class SeoInfo
    {
        [Key] public long Id { get; set; }
        public long ObjectId { get; set; }
        public ObjectType ObjectType { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyword { get; set; }
        public int ContentAngleTag { get; set; }
        public ContentFormatTag ContentFormatTag { get; set; }
        public SourceTag SourceTag { get; set; }
        public string? OgTitle { get; set; }
        public string? OgImage { get; set; }
        public string? OgDescription { get; set; }
        public string? TitleLink { get; set; }
        public string? BackLink { get; set; }
        public string? TitleSeo { get; set; }
        public string? Seokeyword { get; set; }
    }
}