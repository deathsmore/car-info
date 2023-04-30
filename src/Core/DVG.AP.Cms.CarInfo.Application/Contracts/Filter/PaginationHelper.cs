using DVG.AutoPortal.Core.Infrastructures.Base;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Filter
{
    public class PaginationHelper
    {
        public static int DefaultPage => 1;
        public static int DefaultPageSize => 10;

        public static int CalculateTake(int pageSize)
        {
            return pageSize <= 0 ? DefaultPageSize : pageSize;
        }

        public static int CalculateSkip(int pageSize, int page)
        {
            page = page <= 0 ? DefaultPage : page;

            return CalculateTake(pageSize) * (page - 1);
        }

        public static int CalculateTake(PagingParam baseFilter)
        {
            return CalculateTake(baseFilter.PageSize);
        }

        public static int CalculateSkip(PagingParam baseFilter)
        {
            return CalculateSkip(baseFilter.PageSize, baseFilter.PageNumber);
        }
    }
}