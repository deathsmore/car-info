using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarImage
{
    public sealed class CarImageSpec : Specification<Domain.Entities.CarImage, Domain.Entities.CarImage>
    {
        //T-TEMP
        //public void GetByNewCar(long newcarId)
        //{
        //    Query.Select(s => s)
        //        .Where(s => (s.ObjectId == newcarId && s.ImageOfObject == ImageOfObject.ImageOfNewCar))
        //        .AsNoTracking();
        //}
    }
}