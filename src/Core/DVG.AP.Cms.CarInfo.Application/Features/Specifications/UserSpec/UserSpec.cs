using DVG.AP.Cms.CarInfo.Domain.CommonEntities;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.UserSpec
{
    public class UserSpec : Specification<User, User>
    {
        public UserSpec()
        {
        }

        public UserSpec(ICollection<int> ids)
        {
            Query.Select(u => new User()
            {
                Id = u.Id,
                DisplayName = u.DisplayName
            }).Where(u => ids.Contains(u.Id))
                .AsNoTracking();
        }

        public void GetUser(List<int> ids)
        {
            Query.Select(u => new User()
            {
                Id = u.Id,
                DisplayName = u.DisplayName
            }).Where(u => ids.Contains(u.Id))
                .AsNoTracking();
        }
    }
}
