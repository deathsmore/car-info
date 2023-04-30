using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.Dtos;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail;



namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface IVariantRepository : IRepository<Domain.Entities.Variant>
    {
        Task<PagedList<FilterVariantVm>> FilterAsync(FilterVariantParameter paramFilter);
        Task<VariantDetailVm> GetDetail(int id);
        Task<List<CarStructureDto>> ListCarStructureDtoAsync(List<int>? brandIds = null, List<int>? modelIds = null,
            List<int>? variantIds = null);

        Task<List<int>> GetListIdByBrandIds(List<int> brandIds);
        Task<List<int>> GetListIdByModelIds(List<int> modelIds);
        Task<ICollection<Domain.Entities.Variant>> GetListHaveActiveNewCarAsync(int modelId);
        Task<ICollection<Domain.Entities.Variant>> GetListAsync(int modelId);
    }
}
