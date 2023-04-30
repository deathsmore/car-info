using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface ISegmentRepository : IRepository<Domain.Entities.Segment>
    {
        Task<PagedList<FilterSegmentVm>> FilterAsync(FilterSegmentParameter paramFilter);
    }
}
