using DVG.AP.Cms.CarInfo.Application.Contracts.Dtos.Common;
using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Common
{
    public interface IUserRepository
    {
        Task<PagedList<UserFilterDto>> Gets(UserFilterParam param);
    }
}
