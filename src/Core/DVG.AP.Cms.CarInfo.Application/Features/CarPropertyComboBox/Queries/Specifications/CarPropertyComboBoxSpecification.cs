using DVG.AP.Cms.CarInfo.Application.Contracts.Filter;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;

public sealed class CarPropertyComboBoxSpecification : Specification<Domain.Entities.CarPropertyComboBox>
{
    public CarPropertyComboBoxSpecification(IEnumerable<int> comboBoxIds)
    {
        Query
            .Where(cb => comboBoxIds.Contains(cb.Id))
            .Include(cb => cb.CarPropertyComboboxOptions)
            .AsNoTracking();
    }

    public CarPropertyComboBoxSpecification()
    {
    }

    public void NoTracking()
    {
        Query.AsNoTracking();
    }

    public void Filter(string? name, short? status)
    {
        if (status.HasValue)
            Query.Where(c => c.Status == status);
        if (!string.IsNullOrEmpty(name))
            Query.Search(c => c.Name.ToLower(), "%" + name!.ToLower() + "%");
        Query.OrderByDescending(c => c.Id);
    }

    public void Paging(int pageNumber, int pageSize)
    {
        Query.Skip(PaginationHelper.CalculateSkip(pageSize, pageNumber))
            .Take(PaginationHelper.CalculateTake(pageSize));
    }
}

public class CarPropertyComboBoxSingleSpec : Specification<Domain.Entities.CarPropertyComboBox>,
    ISingleResultSpecification
{
    public CarPropertyComboBoxSingleSpec()
    {
    }

    public void NoTracking()
    {
        Query.AsNoTracking();
    }

    public void GetById(int id)
    {
        Query.Include(cb => cb.CarPropertyComboboxOptions)
            .Where(cb => cb.Id == id);
    }
}