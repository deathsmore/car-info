using DVG.AutoPortal.Specification;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    Task<int> AddRangeAsync(IEnumerable<T> entities);
}

/// <inheritdoc/>
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}