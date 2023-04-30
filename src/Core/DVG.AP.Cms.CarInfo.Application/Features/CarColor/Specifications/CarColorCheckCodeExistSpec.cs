using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Specifications
{
    public class CarColorCheckCodeExistSpec : Specification<Domain.Entities.CarColor>,
    ISingleResultSpecification<Domain.Entities.CarColor>
    {
        public CarColorCheckCodeExistSpec(string code)
        {
            Query
                .Where(ci => ci.Code == code)
                .AsNoTracking();
        }
    }
}
