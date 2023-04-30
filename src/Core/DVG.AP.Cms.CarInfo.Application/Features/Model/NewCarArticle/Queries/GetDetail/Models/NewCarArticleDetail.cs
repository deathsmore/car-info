using System.Text.Json.Serialization;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models
{
    public class NewCarArticleGetDetailDto
    {
        public string Id { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? Sapo { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsDCMA { get; set; }
        public bool IsAMP { get; set; }
        public bool IsHot { get; set; }
        //public NewCarSeoInfoDetail SEOInfo { get; set; } = new NewCarSeoInfoDetail();
        public NewCarSEOInfos NewCarSEOInfos { get; set; } = new NewCarSEOInfos();
        public NewCarArticleType Type { get; set; }
        public double Rate { get; set; }
        public bool IsShowMenu { get; set; }
        public int AuthorId { get; set; }
        public NewCarArticleStatus Status { get; set; }
        private int _brandId;

        public int BrandId
        {
            get
            {
                return _brandId;
                //return Type switch
                //{
                //    NewCarArticleType.Brand => ObjectId.ToString().ToInt(),
                //    NewCarArticleType.Model => ParentOfObjectId.ToString().ToInt(),
                //    _ => _brandId
                //};
            }
            set => _brandId = value;
        }

        private int _modelId;

        public int ModelId
        {
            get
            {
                return _modelId;
                //return Type switch
                //{
                //    NewCarArticleType.Model => ObjectId.ToString().ToInt(),
                //    NewCarArticleType.CarInfo => ParentOfObjectId.ToString().ToInt(),
                //    _ => _modelId
                //};
            }
            set => _modelId = value;
        }

        public int VariantId { get; set; }

        [JsonIgnore] public long ObjectId { get; set; }
        [JsonIgnore] public long ParentOfObjectId { get; set; }
        public string? Avatar { get; set; }
        public List<CarImageDto> CarImages { get; set; } = new List<CarImageDto>();
        public bool IsImage360 { get; set; }
        public bool IsShowColorImages { get; set; }
        public List<ContentDetailDto> Contents { get; set; } = new List<ContentDetailDto>();
        public string? BrochureFile { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsOwnerReview { get; set; }
        public string LabelDisplay { get; set; }
        public CarState CarState { get; set; }
        public List<NewCarFAQs> NewCarFAQs { get; set; } = new List<NewCarFAQs>();
        
    }
}