using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Queries.GetColors
{
    public class GetAllCarColorQueryHandler : IRequestHandler<GetAllCarColorQuery,
            IReadOnlyList<GetAllCarColorVm>>
    {
        private readonly IRepository<Domain.Entities.CarColor> _carColorRepository;
        private readonly IMapper _mapper;

        public GetAllCarColorQueryHandler(IRepository<Domain.Entities.CarColor> carColorRepository, IMapper mapper)
        {
            _carColorRepository = carColorRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GetAllCarColorVm>> Handle(GetAllCarColorQuery request,
    CancellationToken cancellationToken)
        {
            var lstCarColors = await _carColorRepository.ListAsync(cancellationToken);

            if (!lstCarColors.Any())
            {
                throw new NotFoundException(nameof(Domain.Entities.CarColor),
                    new {  });
            }
            var result = _mapper.Map<IReadOnlyList<GetAllCarColorVm>>(lstCarColors);

            return result;
        }
    }
}
