using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories
{
    public class NewCarBoxDetailRepository: Repository<NewCarBoxDetail>, INewCarBoxDetailRepository
    {
        public NewCarBoxDetailRepository(CarInfoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<NewCarBoxDetailItemDto>> GetByNewCarBox(int newCarBoxId)
        {

            var selectNewCarModel = (from d in CarInfoDbContext.NewCarBoxDetails
                                     join ncm in CarInfoDbContext.NewCarModels
                                     on d.NewCarArticleId equals ncm.Id
                                     where (d.NewCarBoxId == newCarBoxId && d.ObjectType == Domain.Enums.NewCarArticleType.Model)
                                     select new NewCarBoxDetailItemDto()
                                     {
                                         Id = d.Id,
                                         NewCarBoxId = newCarBoxId,
                                         ObjectId = d.ObjectId,
                                         NewCarArticleId = ncm.Id,
                                         NewCarArticleTitle = ncm.Title,
                                         Ordinal = d.Ordinal,
                                         ObjectType = d.ObjectType,
                                         NewCarStatus = ncm.Status,
                                         PublishedDate = ncm.PublishedDate
                                     });

            var selectNewCarVariant = (from d in CarInfoDbContext.NewCarBoxDetails
                                       join ncv in CarInfoDbContext.NewCarVariants
                                       on d.NewCarArticleId equals ncv.Id
                                       where (d.NewCarBoxId == newCarBoxId && d.ObjectType == Domain.Enums.NewCarArticleType.Variant)
                                       select new NewCarBoxDetailItemDto()
                                       {
                                           Id = d.Id,
                                           NewCarBoxId = newCarBoxId,
                                           ObjectId = d.ObjectId,
                                           NewCarArticleId = ncv.Id,
                                           NewCarArticleTitle = ncv.Title,
                                           Ordinal = d.Ordinal,
                                           ObjectType = d.ObjectType,
                                           NewCarStatus = ncv.Status,
                                           PublishedDate = ncv.PublishedDate
                                       });

            var result = selectNewCarModel.AsQueryable()
                         .Union(selectNewCarVariant.AsQueryable())
                         .OrderBy(n => n.Ordinal);

            return await result.ToListAsync();
        }
    }
}
