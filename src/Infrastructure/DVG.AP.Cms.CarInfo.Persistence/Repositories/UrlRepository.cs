using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AP.Cms.CarInfo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class UrlRepository: CommonRepository<Url>, IUrlRepository
{
    public UrlRepository(CommonDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Url> GetBySlug(string slug)
    {
        var query = CommonDbContext.Urls
                    .Where(u => slug != null && u.Slug.ToLower() == slug.ToLower())
                    .Select(u => u);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Url> GetByObject(long objectId, ObjectType objectType)
    {
        var query = CommonDbContext.Urls
                    .Where(u => u.ObjectId == objectId && u.ObjectType == objectType)
                    .Select(u => u);
        return await query.FirstOrDefaultAsync();
    }
}