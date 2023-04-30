using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

using MediatR;


namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.Filter
{
    public class FilterVariantQuery : IRequest<PagedList<FilterVariantVm>>
    {
        public FilterVariantParameter FilterVariantParameter { get; set; }
    }

    public class FilterVariantParameter
    {
        public string Keyword { get; set; } = string.Empty;
        public int BrandId { get; set; } = 0;
        public int ModelId { get; set; } = 0;
        public ActiveStatus Status { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value == 0 ? _pageSize : value;
        }
    }

    public class FilterVariantVm
    {
        public string Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string? ModelName { get; set; }
        public string? Name { get; set; }
        public ActiveStatus Status { get; set; }
        public bool IsVirtual { get; set; }
    }
}
