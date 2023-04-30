using DVG.AP.Cms.CarInfo.Application.Contracts.Infrastructures.Utilities;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;

using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.Filter
{
    public class FilterCarColorQueryHandler : IRequestHandler<FilterCarColorQuery, PagedList<FilterCarColorVm>>
    {
        private readonly ICarColorRepository _carColorRepository;

        public FilterCarColorQueryHandler(ICarColorRepository carColorRepository)
        {
            _carColorRepository = carColorRepository;
        }

        public async Task<PagedList<FilterCarColorVm>> Handle(FilterCarColorQuery request,
            CancellationToken cancellationToken)
        {

            return await _carColorRepository.FilterAsync(request.CarColorParameter);
        }
    }
}
