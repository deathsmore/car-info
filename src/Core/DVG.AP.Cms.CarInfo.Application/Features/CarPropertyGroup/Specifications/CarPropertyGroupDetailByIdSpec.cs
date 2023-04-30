using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;

public sealed class CarPropertyGroupDetailByIdSpec : Specification<Domain.Entities.CarPropertyGroup>,
    ISingleResultSpecification<Domain.Entities.CarPropertyGroup>
{
    public CarPropertyGroupDetailByIdSpec(int id)
    {
        Query
            .Include(cp => cp.CarProperties)
            .Where(cp => cp.Id == id);
    }
}