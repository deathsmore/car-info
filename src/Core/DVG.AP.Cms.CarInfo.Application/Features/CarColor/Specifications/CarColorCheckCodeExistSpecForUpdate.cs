using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Specifications
{
    public class CarColorCheckCodeExistSpecForUpdate : Specification<Domain.Entities.CarColor>,
    ISingleResultSpecification<Domain.Entities.CarColor>
    {
        public CarColorCheckCodeExistSpecForUpdate(int id, string code)
        {
            Query
                .Where(ci => ci.Code == code && ci.Id != id)
                .AsNoTracking();
        }
    }
}
