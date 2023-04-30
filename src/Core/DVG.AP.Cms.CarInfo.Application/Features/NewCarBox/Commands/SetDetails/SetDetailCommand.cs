using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Commands.SetDetails
{
    public class SetDetailsCommand: IRequest<int>
    {
        public SetDetailsCommand(NewCarBoxSetDetail newCarBoxSetDetail, int userId)
        {
            this.NewCarBoxSetDetail = newCarBoxSetDetail;
            this.NewCarBoxSetDetail.Items.ForEach(bd => bd.Init(userId));
        }
        public NewCarBoxSetDetail NewCarBoxSetDetail { get; set; }
    }

    public class NewCarBoxSetDetail
    {
        public int NewCarBoxId { get; set; }
        public List<NewCarBoxDetailItem> Items { get; set; } = new();
    }

    public class  NewCarBoxDetailItem
    {
        public void Init(int userId)
        {
            CreatedDate = DateTime.Now;
            CreatedBy = userId;
        }
        public int Id { get; set; }
        public string NewCarArticleId { get; set; }
        public short Ordinal { get; set; }
        public string ObjectId { get; set; }
        public NewCarArticleType ObjectType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
