using DVG.AP.Cms.CarInfo.Application.Contracts.Extensions;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update
{
    public class UpdateNewCarArticleCommand : IRequest<int>
    {
        public UpdateNewCarArticleCommand(NewCarArticleForUpdate newCarForUpdate, long id, int currentUserId)
        {
            NewCarArticle = newCarForUpdate;
            NewCarArticle.InitDefaultValues(id, currentUserId);
        }
        public NewCarArticleForUpdate NewCarArticle { get; set; }
    }

    public class NewCarArticleForUpdate
    {
        public void InitDefaultValues(long id, int currentUserId)
        {
            Id = id;
            ModifiedBy = currentUserId;
            ModifiedDate = DateTime.Now;
            PublishedDateSpan = PublishedDate.HasValue ? ((DateTime)PublishedDate).DateTimeToUnixTime() : 0;
            if (Status == NewCarArticleStatus.Approved) PublishedBy = currentUserId;
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string? Url { get; set; }
        public string? Sapo { get; set; }
        public DateTime? PublishedDate { get; set; }
        public long PublishedDateSpan { get; set; }
        public int? PublishedBy { get; private set; }
        public bool IsDCMA { get; set; }
        public bool IsAMP { get; set; }
        //public SEOInfoUpdate SEOInfo { get; set; } = new();
        public NewCarSEOInfos NewCarSEOInfos { get; set; } = new();
        public NewCarArticleType Type { get; set; }
        public double? Rate { get; set; }
        public bool IsShowMenu { get; set; }
        public int? AuthorId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int VariantId { get; set; }
        public string? Avatar { get; set; }
        public List<ImageForUpdate> Images { get; set; } = new();
        public bool IsImage360 { get; set; }
        public bool IsShowColorImages { get; set; }
        public List<ContentForUpdate> Contents { get; set; } = new();
        public string BrochureFile { get; set; }
        public string? VideoUrl { get; set; }
        public bool IsOwnerReview { get; set; }
        public CarState CarState { get; set; }
        public NewCarArticleStatus Status { get; set; }
        public int ModifiedBy { get; private set; }
        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }
        public string? LabelDisplay { get; set; }
        [JsonIgnore]
        public bool HasNewCarVariant { get; set; }
        [JsonIgnore]
        public bool HasNewCarModel { get; set; }
        
        public List<NewCarFAQs> NewCarFAQs { get; set; } = new List<NewCarFAQs>();

    }
}
