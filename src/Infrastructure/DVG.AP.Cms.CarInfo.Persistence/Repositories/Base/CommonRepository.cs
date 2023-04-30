using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AutoPortal.EFCore6;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories.Base
{
    public class CommonRepository<T> : RepositoryBase<T>, ICommonRepository<T> where T : class
    {
        protected readonly CommonDbContext CommonDbContext;

        public CommonRepository(CommonDbContext dbContext) : base(dbContext)
        {
            CommonDbContext = dbContext;
        }
    }
}
