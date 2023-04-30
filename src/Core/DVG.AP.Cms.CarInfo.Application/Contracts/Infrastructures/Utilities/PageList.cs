namespace DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities
{
    public class PagedList<T> where T : class
    {
        public PagedList(int currentPage, int pageSize, int totalCount, IEnumerable<T> collections)
        {
            TotalCount = totalCount;
            Collections = collections;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; }

        public PagedList()
        {
            TotalCount = 0;
            Collections = new List<T>();
            CurrentPage = 0;
            TotalPages = 0;
        }

        public int TotalCount { get; }
        public IEnumerable<T> Collections { get; }
    }
}