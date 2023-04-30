using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.Variant
{
    public sealed class VariantSpec : Specification<Domain.Entities.Variant, Domain.Entities.Variant>
    {
        public VariantSpec()
        {

        }
        public void GetByModel(int modelId)
        {
            Query.Where(v => v.ModelId == modelId);
        }

        public void Select()
        {
            Query.Select(v => v);
        }
        public void IncludeParrent()
        {
            Query.Include(v => v.Model.Brand);
        }
        public void GetById(int id)
        {
            Query
                .Select(x=> x)
                .Where(v => v.Id == id);
        }
    }
}
