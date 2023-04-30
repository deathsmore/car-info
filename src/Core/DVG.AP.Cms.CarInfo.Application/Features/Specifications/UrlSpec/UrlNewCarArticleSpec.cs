using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;

public sealed class UrlNewCarArticleSpec : Specification<Url>, ISingleResultSpecification
{
    public UrlNewCarArticleSpec(long newcarId)
    {
        Query
            .Where(s => (s.ObjectType == ObjectType.NewCar && s.ObjectId == newcarId))
            .AsNoTracking();
    }

    public void GetByNewCar(long newcarId)
    {
        Query
            .Where(s => (s.ObjectType == ObjectType.NewCar && s.ObjectId == newcarId))
            .AsNoTracking();
    }
}