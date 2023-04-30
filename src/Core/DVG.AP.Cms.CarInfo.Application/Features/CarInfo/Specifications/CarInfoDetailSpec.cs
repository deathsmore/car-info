using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;

public sealed class CarInfoDetailSpec : Specification<Domain.Entities.CarInfo>,
    ISingleResultSpecification<Domain.Entities.CarInfo>
{
    public CarInfoDetailSpec(long id)
    {
        Query
         .Include(ci => ci.Images!.Where(i => i.ImageOfObject == ImageOfObject.ImageOfCarInfo)) 
            .Include(ci => ci.BodyType)
            .Include(ci => ci.FuelType)
            .Include(ci => ci.Transmission)
            .Include(ci => ci.Prices)
            .Include(ci => ci.Variant.Model.Brand)
            .Where(ci => ci.Id == id)
            .AsNoTracking();
    }
}
