using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarInfoPropertyValue
{
    public class CarInfoPropertyValueSpec : Specification<Domain.Entities.CarInfoPropertyValue, Domain.Entities.CarInfoPropertyValue>
    {
        public CarInfoPropertyValueSpec()
        {

        }
        public void GetByCarInfo(long carInfoId)
        {
            Query.Where(s => (s.CarInfoId == carInfoId))
                .AsNoTracking();
        }
        public void Select()
        {
            Query.Select(x => x);
        }
    }
}
