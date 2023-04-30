using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class ModelPropertyValueRepository : Repository<ModelPropertyValue>, IModelPropertyValueRepository
    {
        public ModelPropertyValueRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<ModelPropertyValue>> ListAsync(int modelId)
        {
            var query = CarInfoDbContext.ModelPropertyValues
                        .AsNoTracking()
                        .Where(mv => mv.ModelId == modelId);
            return await query.ToListAsync();
        }
    }
}
