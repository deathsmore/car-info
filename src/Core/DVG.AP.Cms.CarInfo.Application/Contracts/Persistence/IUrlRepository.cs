using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

public interface IUrlRepository: ICommonRepository<Url>
{
    Task<Url> GetBySlug(string slug);
    Task<Url> GetByObject(long objectId, ObjectType objectType);
}