using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class ModelRepository : Repository<Domain.Entities.Model>, IModelRepository
    {
        public ModelRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ICollection<Model>> GetListAsync(int brandId, ActiveStatus status)
        {
            var query = from m in CarInfoDbContext.Models
                        where (brandId == 0 || m.BrandId == brandId)
                        && (status == 0 || m.Status == status)
                        select m;
            return await query.ToListAsync();
        }

        public async Task<ICollection<Model>> GetListByIds(List<int> modelIds)
        {
            var query = CarInfoDbContext.Models
                    .Where(m => modelIds != null && modelIds.Any() && modelIds.Contains(m.Id))
                    .Select(x => x);
            return await query.ToListAsync();
        }

        public async Task<ICollection<Model>> GetListHaveActiveNewCarAsync(int brandId, ActiveStatus status)
        {
            var query = from m in CarInfoDbContext.Models
                        join ncm in CarInfoDbContext.NewCarModels on m.Id equals ncm.ModelId
                        where (brandId == 0 || m.BrandId == brandId)
                        && (status == 0 || m.Status == status)
                        && (ncm.Status == NewCarArticleStatus.Approved && ncm.PublishedDate <= DateTime.Now)
                        select m;
            return await query.ToListAsync();
        }

        public async Task<List<int>> GetListIdByBrand(int brandId)
        {
            var query = CarInfoDbContext.Models
                .AsNoTracking()
                .Where(m => brandId == 0 || m.BrandId == brandId)
                .Select(x => x.Id);
            return await query.ToListAsync();
        }

        public async Task<List<ModelInSegmentVm>> GetListInSegment(int segmentId)
        {
            var query = CarInfoDbContext.Models
                .AsNoTracking()
                .Include(m => m.Brand)
                .Where(m => m.SegmentId == segmentId)
                .Select(m => new ModelInSegmentVm()
                {
                    BrandId = m.BrandId,
                    BrandName = m.Brand.Name,
                    ModelId = m.Id,
                    ModelName = m.Name
                });
            return await query.ToListAsync();
        }
    }
}
