using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence
{
    public interface IModelPropertySummaryRepository : IRepository<Domain.Entities.ModelPropertySummary>
    {
        Task<Domain.Entities.ModelPropertySummary> GetByIdAsync(int id);
    }
}
