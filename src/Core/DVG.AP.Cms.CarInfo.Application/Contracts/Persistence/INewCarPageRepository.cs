using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface INewCarPageRepository : IRepository<NewCarPage>
    {
        Task<PagedList<NewCarPageFilterVm>> FilterAsync(NewCarPageFilterParam paramFilter);
    }
}
