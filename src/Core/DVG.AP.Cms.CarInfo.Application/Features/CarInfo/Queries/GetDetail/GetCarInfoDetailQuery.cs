using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail
{
    public class GetCarInfoDetailQuery : IRequest<CarInfoGetDetailVm>
    {
        public long Id { get; set; }
    }
}
