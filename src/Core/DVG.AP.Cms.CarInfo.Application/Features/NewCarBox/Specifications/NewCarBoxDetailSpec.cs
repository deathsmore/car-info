using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Specifications
{
    public class NewCarBoxDetailSpec: Specification<NewCarBoxDetail, NewCarBoxDetail>, INewCarBoxDetailSpec
    {
        public void GetByNewCarBoxId(int newCarBoxId)
        {
            Query.Where(b => b.NewCarBoxId == newCarBoxId);
        }
        public void SelectAllField()
        {
            Query.Select(b => b);
        }
    }

    public interface INewCarBoxDetailSpec 
    {
        void GetByNewCarBoxId(int newCarBoxId);
        void SelectAllField(); 
    }
    
}
