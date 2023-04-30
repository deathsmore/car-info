using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AP.Cms.CarInfo.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DVG.AP.Cms.CarInfo.Persistence.Repositories;

public class SeoInfoRepository : CommonRepository<SeoInfo>, ISeoInfoRepository
{
    public SeoInfoRepository(CommonDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<long>> GetListNewCarArticleId(ContentFormatTag contentFormatTag,
        int contentAngleTag, SourceTag sourceTag)
    {
        var query = CommonDbContext.SeoInfos.AsNoTracking()
            .Where(si =>
                (si.ObjectType == ObjectType.NewCar)
                && (contentFormatTag == ContentFormatTag.All || si.ContentFormatTag == contentFormatTag)
                && (contentAngleTag == 0 || si.ContentAngleTag == contentAngleTag)
                && (sourceTag == SourceTag.All || si.SourceTag == sourceTag)
            )
            .Select(si => si.ObjectId);
        return await query.ToListAsync();
    }
}