using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Queries.GetDetail;

public class GetCarInfoPropertyValueDetailQuery : IRequest<CarInfoDetailIncludePropertyValueVm>
{
    public long CarInfoId { get; }

    public GetCarInfoPropertyValueDetailQuery(string carInfoId)
    {
        CarInfoId = carInfoId.ToLong();
    }
}