using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    internal class BrandRepository : Repository<Brand>,
        IBrandRepository
    {
        public BrandRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Brand?> GetByModel(int modelId)
        {
            var query = from model in CarInfoDbContext.Models
                join brand in CarInfoDbContext.Brands on model.BrandId equals brand.Id
                where model.Id == modelId
                select brand;
            query = query.AsNoTracking();

            
            return await query.FirstOrDefaultAsync();
        }
    }
}