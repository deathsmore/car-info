using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.Filter
{
    public class FilterSegmentQueryHandler : IRequestHandler<FilterSegmentQuery, PagedList<FilterSegmentVm>>
    {
        private readonly ISegmentRepository _segmentRepository;
        public FilterSegmentQueryHandler(ISegmentRepository segmentRepository)
        {
            _segmentRepository = segmentRepository;
        }
        public async Task<PagedList<FilterSegmentVm>> Handle(FilterSegmentQuery request, CancellationToken cancellationToken)
        {
            return await _segmentRepository.FilterAsync(request.FilterParams);
        }
    }
}
