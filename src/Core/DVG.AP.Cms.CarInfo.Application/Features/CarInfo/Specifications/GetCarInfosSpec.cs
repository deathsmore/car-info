using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;

public sealed class GetCarInfosSpec : Specification<Domain.Entities.CarInfo>
{
    public GetCarInfosSpec()
    {

    }
    public GetCarInfosSpec(int variantId, ActiveStatus status)
    {
        Query
            .Where(ci => ci.VariantId == variantId && (status == ActiveStatus.All || ci.Status == status))
            .Where(ci => true && (status == ActiveStatus.All || ci.Status == status))
            .AsNoTracking();

    }
    public void GetByVariant(int variantId)
    {
        Query.Where(ci => ci.VariantId == variantId);
    }
    public void GetByYear(int year)
    {
        Query.Where(ci => ci.Year == year);
    }
}