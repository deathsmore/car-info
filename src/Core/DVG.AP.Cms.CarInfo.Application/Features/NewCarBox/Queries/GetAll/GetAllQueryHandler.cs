using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IReadOnlyList<NewCarBoxVm>>
    {
        private readonly IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> _newCarBoxRepository;

        public GetAllQueryHandler(
            IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> newCarBoxRepository
        )
        {
            this._newCarBoxRepository = newCarBoxRepository;
        }

        public async Task<IReadOnlyList<NewCarBoxVm>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
           
            var newCarBoxes = await _newCarBoxRepository.ListAsync( cancellationToken);
            return newCarBoxes.Select(b => new NewCarBoxVm()
            {
                Id = b.Id,
                Name = b.Name,
                NumberDisplay = b.NumberDisplay,
                Ordinal = b.Ordinal,
                Status = b.Status,
                NewCarType = b.NewCarType
            }).ToList();
        }
    }
}