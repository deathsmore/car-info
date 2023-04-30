using DVG.AP.Cms.CarInfo.Domain.Entities.Enums;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos
{
    public class GetCarInfoByModelAndStatusQuery : IRequest<IReadOnlyList<CarInfoVm>>
    {
        public int VariantId { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
