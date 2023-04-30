using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle
{
    public class NewCarArticleSpec : Specification<Domain.Entities.NewCarArticle, Domain.Entities.NewCarArticle>
    {
        public NewCarArticleSpec()
        {
        }

        public void GetByIdIncludeRelations(long id)
        {
            Query.Include(n => n.Contents)
                //.Include(n => n.Images!.Where(i => i.ImageOfObject == ImageOfObject.ImageOfNewCar))//T-TEMP
                .Where(n => n.Id == id);
        }

        public void GetByObjectIdAndType(long objectId, NewCarArticleType type)
        {
            Query.Where(n => n.ObjectId == objectId && n.Type == type);
        }

        public void Select()
        {
            Query.Select(n => n);
        }
    }
}