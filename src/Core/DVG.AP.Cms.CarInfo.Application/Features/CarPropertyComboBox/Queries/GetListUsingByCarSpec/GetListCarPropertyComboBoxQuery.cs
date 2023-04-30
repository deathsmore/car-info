using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.GetListUsingByCarSpec;

public class GetListCarPropertyComboBoxUsedQuery : IRequest<IReadOnlyList<CarPropertyComboBoxGetListUsedVm>>
{
}