using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll
{
    public class GetAllQuery: IRequest<IReadOnlyList<NewCarBoxVm>>
    {

    }

    public class NewCarBoxVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberDisplay { get; set; }
        public int Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
        public NewCarArticleType NewCarType { get; set; }
    }
}
