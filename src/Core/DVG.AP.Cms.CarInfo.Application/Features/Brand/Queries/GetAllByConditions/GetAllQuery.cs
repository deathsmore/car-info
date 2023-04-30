using DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Brand.Queries.GetAllByConditions
{
    public class GetAllQuery: IRequest<IReadOnlyList<BrandVm>>
    {
        public ActiveStatus Status { get; set; }
    }
}
