using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec
{
    public sealed class UrlCarInfoSpec : Specification<Url, Url>
    {
        public UrlCarInfoSpec()
        {

        }
        public void GetByCarInfo(long carInfoId)
        {
            Query.Select(s => s)
                .Where(s => (s.ObjectType == ObjectType.CarInfo && s.ObjectId == carInfoId))
                .AsNoTracking();
        }
    }
}
