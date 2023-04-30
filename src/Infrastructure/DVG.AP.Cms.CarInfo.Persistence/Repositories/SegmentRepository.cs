using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class SegmentRepository : Repository<Domain.Entities.Segment>, ISegmentRepository
    {
        public SegmentRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedList<FilterSegmentVm>> FilterAsync(FilterSegmentParameter paramFilter)
        {
            var keywordSearch = ($"%{paramFilter.Keyword}%").ToLower();

            var query = from segment in CarInfoDbContext.Segments
                        where ((paramFilter.Status == ActiveStatus.All || segment.Status == paramFilter.Status)
                              && (string.IsNullOrEmpty(paramFilter.Keyword) ||
                                  EF.Functions.Like(segment.Id.ToString().ToLower(), keywordSearch) || (segment.Name != null &&
                                      EF.Functions.Like(segment.Name.ToLower(), keywordSearch))))
                        orderby segment.CreatedDate descending, segment.Id descending
                        select new FilterSegmentVm()
                        {
                            Id = segment.Id,
                            Name = segment.Name,
                            Ordinal = segment.Ordinal,
                            Status = segment.Status
                        };

            query = query.AsNoTracking();

            var totalRecord = await query.CountAsync();

            var collections = new List<FilterSegmentVm>();
            if (totalRecord > 0)
            {
                collections = await query.Skip((paramFilter.PageNumber - 1) * paramFilter.PageSize)
                    .Take(paramFilter.PageSize).ToListAsync();
            }

            var result = new PagedList<FilterSegmentVm>(paramFilter.PageNumber, paramFilter.PageSize, totalRecord,
                collections);
            return result;
        }
    }
}
