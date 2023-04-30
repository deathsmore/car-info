using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Specification;
using DVG.AutoPortal.Specification.Builder;

namespace DVG.AP.Cms.CarInfo.Application.Features.Specifications.SEOInfo;

public sealed class SeoInfoNewCarSpec : Specification<SeoInfo, SeoInfo>
{
    public SeoInfoNewCarSpec(ContentFormatTag contentFormatTag, int contentAngleTag, SourceTag sourceTag)
    {
        Query
            .Select(s => new SeoInfo() {Id = s.Id, ObjectId = s.ObjectId})
            .Where(s => s.ObjectType == ObjectType.NewCar).AsNoTracking();
        if (contentFormatTag != 0)
            Query.Where(s => s.ContentFormatTag == contentFormatTag);
        if (contentAngleTag != 0)
            Query.Where(s => s.ContentAngleTag == contentAngleTag);

        if (sourceTag != 0)
            Query.Where(s => s.SourceTag == sourceTag);
    }
    public SeoInfoNewCarSpec()
    {

    }
    public void GetByNewCar(long newcarId, ObjectType objectType)
    {
        Query.Select(s => s)
        .Where(s => (s.ObjectType == objectType && s.ObjectId == newcarId))
        .AsNoTracking();
    }
}