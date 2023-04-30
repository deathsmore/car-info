using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.Transmission
{
    public class TransmissionSpec : Specification<Domain.Entities.Transmission, Domain.Entities.Transmission>
    {
        public TransmissionSpec()
        {

        }

        public void GetByStatus(ActiveStatus status)
        {
            Query.Where(t => status == ActiveStatus.All || t.Status == status);
        }

        public void Select()
        {
            Query.Select(t => t);
        }
    }
}
