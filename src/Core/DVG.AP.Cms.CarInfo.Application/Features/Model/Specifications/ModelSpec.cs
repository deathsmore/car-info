using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.Specifications
{
    public class ModelSpec : Specification<Domain.Entities.Model, Domain.Entities.Model>
    {
        public ModelSpec()
        {

        }
        public void GetByStatus(ActiveStatus status)
        {
            Query.Where(m => status == ActiveStatus.All || m.Status == status);
        }

        public void GetByBrand(int brandId)
        {
            Query.Where(m => brandId == 0 || m.BrandId == brandId);
        }

        public void Select()
        {
            Query.Select(m => m);
        }
    }
}
