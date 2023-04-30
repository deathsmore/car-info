using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.Filter
{
    public class FilterCarInfoParameter
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
}