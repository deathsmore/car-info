using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Specifications;

public sealed class CarPropertyGroupSpecification : Specification<Domain.Entities.CarPropertyGroup>
{
    public CarPropertyGroupSpecification()
    {
        Query.AsNoTracking()
            .OrderBy(c => c.Ordinal)
            .ThenByDescending(c => c.CreatedDate);
    }

    public void IncludeChild()
    {
        Query.Include(c => c.CarProperties);
    }

    public void Filter(string? name, short? status)
    {
        if (status != null && status != 0)
        {
            Query.Where(cp => (cp.Status == status));
        }

        if (!string.IsNullOrEmpty(name))
        {
            Query.Search(cp => cp.Name!.ToLower(), "%" + name.ToLower() + "%");
        }
    }
}