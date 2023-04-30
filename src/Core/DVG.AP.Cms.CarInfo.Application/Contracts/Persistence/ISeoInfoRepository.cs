using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface ISeoInfoRepository : ICommonRepository<SeoInfo>
{
    Task<List<long>> GetListNewCarArticleId(ContentFormatTag contentFormatTag,
        int contentAngleTag, SourceTag sourceTag);
}