using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Specifications
{
    public class NewCarBoxSpec : Specification<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox>
    {
        public NewCarBoxSpec()
        {

        }

        public void SelectAllField()
        {
            Query.Select(b => b);
        }
    }
}
