using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.BodyType.Specifications
{
    public class BodyTypeSpec: Specification<Domain.Entities.BodyType, Domain.Entities.BodyType>
    {
        public BodyTypeSpec()
        {
            
        }

        public void GetByStatus(ActiveStatus status)
        {
            Query.Where(m => status == ActiveStatus.All || m.Status == status);
        }

        public void Select()
        {
            Query.Select(b => b);
        }
    }
}
