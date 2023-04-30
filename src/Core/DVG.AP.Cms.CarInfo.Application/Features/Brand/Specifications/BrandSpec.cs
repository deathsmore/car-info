using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Brand.Specifications
{
    public class BrandSpec : Specification<Domain.Entities.Brand, Domain.Entities.Brand>
    {
        public BrandSpec()
        {

        }
        public void GetByStatus(ActiveStatus status)
        {
            Query.Where(b => status == ActiveStatus.All || b.Status == status);
        }

        public void Select()
        {
            Query.Select(b => b);
        }
    }
}
