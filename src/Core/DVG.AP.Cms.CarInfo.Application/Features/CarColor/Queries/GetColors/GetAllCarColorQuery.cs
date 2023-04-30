using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.GetColors
{
    public class GetAllCarColorQuery : IRequest<IReadOnlyList<GetAllCarColorVm>>
    {
    }
}
