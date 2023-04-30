using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions
{
    public class GetAllQuery: IRequest<IReadOnlyList<ModelVm>>
    {
        public int BrandId { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
