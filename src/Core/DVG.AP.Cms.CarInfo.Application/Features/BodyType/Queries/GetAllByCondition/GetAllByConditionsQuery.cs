using DVG.AP.Cms.CarInfo.Application.Features.BodyType.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.BodyType.Queries.GetAllByCondition
{
    public class GetAllByConditionsQuery : IRequest<IReadOnlyList<BodyTypeVm>>
    {
        public ActiveStatus Status { get; set; }
    }
}
