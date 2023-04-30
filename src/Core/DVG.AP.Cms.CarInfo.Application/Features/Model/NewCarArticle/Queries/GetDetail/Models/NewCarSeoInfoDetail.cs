using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models
{
    public class NewCarSeoInfoDetail
    {
        public NewCarSeoInfoDetail()
        {
        }

        public NewCarSeoInfoDetail(NewCarSEOInfos seo)
        {
            NewCarArticleId = seo.NewCarArticleId;
            Type = seo.Type;
            MetaDescription = seo.MetaDescription;
            MetaKeyword = seo.MetaKeyword;
            MetaTitle = seo.MetaTitle;
            
            SourceTag = seo.SourceTag;
            ContentAngleTag = seo.ContentAngleTag;
            ContentFormatTag = seo.ContentFormatTag;
            SEOKeyword = seo.SEOKeyword;
        }

        public long NewCarArticleId { get; set; }
        public short Type { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public int ContentAngleTag { get; set; }
        public ContentFormatTag ContentFormatTag { get; set; }
        public SourceTag SourceTag { get; set; }
        public string TitleSeo { get; set; }
        public string SEOKeyword { get; set; }
    }
}
