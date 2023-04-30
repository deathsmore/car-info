namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter
{
    public class FilterCarColorParameter
    {
        public string Keyword { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value == 0 ? _pageSize : value;
        }
    }
}
