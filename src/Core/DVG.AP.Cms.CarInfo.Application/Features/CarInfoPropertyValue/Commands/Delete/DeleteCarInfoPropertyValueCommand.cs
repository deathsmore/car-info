using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Delete;

public class DeleteCarInfoPropertyValueCommand : IRequest
{
    public DeleteCarInfoPropertyValueCommand(string carInfoId)
    {
        CarInfoId = carInfoId.ToLong();
    }

    public long CarInfoId { get; }
}