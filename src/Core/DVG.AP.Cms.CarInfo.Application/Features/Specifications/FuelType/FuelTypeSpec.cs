using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.FuelType
{
    public class FuelTypeSpec : Specification<Domain.Entities.FuelType, Domain.Entities.FuelType>
    {
        public FuelTypeSpec()
        {

        }

        public void Select()
        {
            Query.Select(ft => ft);
        }
    }
}
