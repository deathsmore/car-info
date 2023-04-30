using DVG.AutoPortal.Specification;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base
{
    public interface ICommonRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
