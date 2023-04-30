using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec
{
    public sealed class UrlNewCarSpec : Specification<Url, Url>
    {
        public UrlNewCarSpec()
        {
        }

        public void GetByNewCar(long newcarId, ObjectType objectType)
        {
            Query.Select(s => s)
                .Where(s => (s.ObjectType == objectType && s.ObjectId == newcarId))
                .AsNoTracking();
        }
    }
}