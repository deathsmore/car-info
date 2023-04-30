using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class ModelPropertySummaryRepository : Repository<ModelPropertySummary>, IModelPropertySummaryRepository
    {
        public ModelPropertySummaryRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ModelPropertySummary> GetByIdAsync(int id)
        {
            return await CarInfoDbContext.ModelPropertySummaries.FindAsync(id);
        }
    }
}
