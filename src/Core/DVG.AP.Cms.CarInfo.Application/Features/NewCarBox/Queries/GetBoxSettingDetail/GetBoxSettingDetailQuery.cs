using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail
{
    public class GetBoxSettingDetailQuery : IRequest<NewCarBoxSettingDetailVm>
    {
        public GetBoxSettingDetailQuery(int newCarBoxId)
        {
            this.NewCarBoxId = newCarBoxId;
        }
        public int NewCarBoxId { get; set; }
    }
    public class NewCarBoxSettingDetailVm
    {
        public int NewCarBoxId { get; set; }
        public List<NewCarBoxDetailItemVm> Items { get; set; } = new();
    }
    public class NewCarBoxDetailItemVm
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userId;
        }
        public int Id { get; set; }
        public int NewCarBoxId { get; set; }
        public short Ordinal { get; set; }
        public string ObjectId { get; set; }
        public NewCarArticleType ObjectType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string NewCarArticleId { get; set; }
        public string NewCarArticleTitle { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CarInfoName { get; set; }
        public NewCarArticleStatus NewCarStatus { get; set; }
        public DateTime? PublishedDate { get; set; }
    }

    public class NewCarBoxDetailItemDto
    {
        public int Id { get; set; }
        public int NewCarBoxId { get; set; }
        public short Ordinal { get; set; }
        public long ObjectId { get; set; }
        public NewCarArticleType ObjectType { get; set; }
        public long NewCarArticleId { get; set; }
        public string NewCarArticleTitle { get; set; }
        public NewCarArticleStatus NewCarStatus { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
