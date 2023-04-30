using Dvg.AP.Cms.NewcarArticle.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.Filter
{
    public class FilterSegmentQuery : IRequest<PagedList<FilterSegmentVm>>
    {
        public FilterSegmentParameter FilterParams { get; set; }
    }

    public class FilterSegmentParameter : PagingParam
    {
        public ActiveStatus Status { get; set; }
    }

    public class FilterSegmentVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
