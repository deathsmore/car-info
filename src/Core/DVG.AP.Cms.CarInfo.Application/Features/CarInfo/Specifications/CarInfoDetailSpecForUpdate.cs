using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;

public sealed class CarInfoDetailSpecForUpdate : Specification<Domain.Entities.CarInfo>,
    ISingleResultSpecification<Domain.Entities.CarInfo>
{
    public CarInfoDetailSpecForUpdate(long id)
    {
        Query
            .Include(ci => ci.Variant)
            .Include(ci => ci.Transmission)
            .Include(ci => ci.Images)//T-TEMP
            .Include(ci => ci.Prices)
            .Where(ci => ci.Id == id);
    }
}