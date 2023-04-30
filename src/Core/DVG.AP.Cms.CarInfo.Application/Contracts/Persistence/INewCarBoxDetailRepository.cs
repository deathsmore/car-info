using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface INewCarBoxDetailRepository : IRepository<NewCarBoxDetail>
    {
        Task<IReadOnlyList<NewCarBoxDetailItemDto>> GetByNewCarBox(int newCarBoxId);
    }
}
