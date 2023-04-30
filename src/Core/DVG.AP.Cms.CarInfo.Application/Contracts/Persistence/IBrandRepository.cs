using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface IBrandRepository : IRepository<Domain.Entities.Brand>
    {
        Task<Brand?> GetByModel(int modelId);
    }
}
