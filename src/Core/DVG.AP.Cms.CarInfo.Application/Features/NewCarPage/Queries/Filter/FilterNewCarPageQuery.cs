using Dvg.AP.Cms.NewcarArticle.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.Filter
{
    public class FilterNewCarPageQuery : IRequest<PagedList<NewCarPageFilterVm>>
    {
        public FilterNewCarPageQuery(NewCarPageFilterParam filterParam)
        {
            FilterParam = filterParam;
        }

        public NewCarPageFilterParam FilterParam { get; set; }
    }

    public class NewCarPageFilterParam : PagingParam
    {
        public NewCarPageType? Type { get; set; }
        public ActiveStatus? Status { get; set; }
    }

    public class NewCarPageFilterVm
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public NewCarPageType Type { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Name { get; set; }
        public ActiveStatus Status { get; set; }
        public short Ordinal { get; set; }
        public bool? IsHot { get; set; }
        public int ObjectId { get; set; }
        public string? Slug { get; set; }
        public int CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string? ModifiedByName { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
