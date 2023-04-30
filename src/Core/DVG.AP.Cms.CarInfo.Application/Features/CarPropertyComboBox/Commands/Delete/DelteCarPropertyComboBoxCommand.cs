using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Commands.Delete;

public class DelteCarPropertyComboBoxCommand:   IRequest
{
    public DelteCarPropertyComboBoxCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}