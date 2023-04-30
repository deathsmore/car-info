using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.Filter;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AutoPortal.Core.Infrastructures.Utilies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class CarPropertyRepository : Repository<CarProperty>, ICarPropertyRepository
    {
        public CarPropertyRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PagedList<CarSpecFilterVm>> FilterCarSpecAsync(CarSpecFilterParam? param)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CarProperty>> GetModelSpecs()
        {
            var query = CarInfoDbContext.CarProperties
                        .AsNoTracking()
                        .Where(cp => cp.IsModelSpec);
            return await query.ToListAsync();
        }
    }
}
