namespace DVG.AP.Cms.CarInfo.Application.Contracts.Filter
{
    public class BaseFilter
    {
      

        private int _pageSize = 20;

        public string? Keyword { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = value == 0 ? this._pageSize : value;
        }
    }
}