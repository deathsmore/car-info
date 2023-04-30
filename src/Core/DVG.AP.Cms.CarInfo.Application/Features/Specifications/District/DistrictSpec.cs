using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.District
{
    public class DistrictSpec : Specification<Domain.Entities.CommonEntities.District, Domain.Entities.CommonEntities.District>
    {
        public void GetByIds(List<int> ids)
        {
            Query.Where(d => ids.Contains(d.Id));
        }

        public void IncludeRelations()
        {
            Query.Include(d => d.City)
                    .ThenInclude(c => c.Region);
        }

        public void Select()
        {
            Query.Select(d => d);
        }
    }
}
