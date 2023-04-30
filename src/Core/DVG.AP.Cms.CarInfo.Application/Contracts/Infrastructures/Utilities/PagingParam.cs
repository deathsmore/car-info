namespace Dvg.AP.Cms.NewcarArticle.Application.Contracts.Infrastructures.Utilities
{
    public abstract class PagingParam
    {
        public string? Keyword { get; set; }
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value == 0 ? _pageSize : value;
        }
    }
}