using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetDetail;

public class GetDetailCarPropertyGroupQuery : IRequest<CarPropertyGroupDetailVm>
{
    public GetDetailCarPropertyGroupQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}