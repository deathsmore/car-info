using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetDetail;

public class GetDetailCarPropertyComboBoxQuery : IRequest<CarPropertyComboBoxDetail>
{
    public GetDetailCarPropertyComboBoxQuery(int id)
    {
        Id = id;
    }

    public  int Id { get;  }
}