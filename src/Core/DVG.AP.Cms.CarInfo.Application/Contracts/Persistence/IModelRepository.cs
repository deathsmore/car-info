using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface IModelRepository : IRepository<Domain.Entities.Model>
    {
        Task<List<int>> GetListIdByBrand(int brandId);
        Task<ICollection<Domain.Entities.Model>> GetListByIds(List<int> modelIds);
        Task<List<ModelInSegmentVm>> GetListInSegment(int segmentId);
        Task<ICollection<Domain.Entities.Model>> GetListHaveActiveNewCarAsync(int brandId, ActiveStatus status);
        Task<ICollection<Domain.Entities.Model>> GetListAsync(int brandId, ActiveStatus status);
    }
}
