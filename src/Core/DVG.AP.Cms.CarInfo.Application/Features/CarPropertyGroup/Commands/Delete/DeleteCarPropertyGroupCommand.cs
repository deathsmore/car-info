using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Delete;

public class DeleteCarPropertyGroupCommand : IRequest
{
    public DeleteCarPropertyGroupCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}