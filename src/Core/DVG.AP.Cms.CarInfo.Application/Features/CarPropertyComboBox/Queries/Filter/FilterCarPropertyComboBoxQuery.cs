using DVG.AutoPortal.Core.Infrastructures.Base;
using DVG.AutoPortal.Core.Infrastructures.Utilies;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyComboBox.Queries.Filter;

public class FilterCarPropertyComboBoxQuery : IRequest<PagedList<CarPropertyComboBoxFilterVm>>
{
    public FilterCarPropertyComboBoxQuery(CarPropertyComboBoxFilterParam comboBoxFilterParam)
    {
        ComboBoxFilterParam = comboBoxFilterParam;
    }

    public CarPropertyComboBoxFilterParam ComboBoxFilterParam { get; }
}

public class CarPropertyComboBoxFilterParam : PagingParam
{
    public short? Status { get; set; }
}