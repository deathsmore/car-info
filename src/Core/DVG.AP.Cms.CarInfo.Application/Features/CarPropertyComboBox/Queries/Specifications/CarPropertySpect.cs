using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Specifications;

public sealed class CarPropertySpecification:  Specification<Domain.Entities.CarProperty, int>
{
    public CarPropertySpecification()
    {
        Query

            .Select(cp => cp.CarPropertyComboBoxId)
            .Where(cp => cp.Type == CarPropertyType.ComboBox)
            .AsNoTracking();
      
    } 
}