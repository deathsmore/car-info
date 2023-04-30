using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.BodyType.Models;
using DVG.AP.Cms.CarInfo.Application.Features.BodyType.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.BodyType.Queries.GetAllByCondition
{
    public class GetAllByConditionsQueryHandler : IRequestHandler<GetAllByConditionsQuery, IReadOnlyList<BodyTypeVm>>
    {
        private readonly IRepository<Domain.Entities.BodyType> _bodyTypeRepository;
        public GetAllByConditionsQueryHandler(
            IRepository<Domain.Entities.BodyType> _bodyTypeRepository
        )
        {
            this._bodyTypeRepository = _bodyTypeRepository;
        }
        public async Task<IReadOnlyList<BodyTypeVm>> Handle(GetAllByConditionsQuery request, CancellationToken cancellationToken)
        {
            var bodyTypeSpec = new BodyTypeSpec();
            bodyTypeSpec.Select();
            bodyTypeSpec.GetByStatus(request.Status);

            var bodyTypes = await _bodyTypeRepository.ListAsync(bodyTypeSpec);
            return bodyTypes.Select(b => new BodyTypeVm()
            {
                Id = b.Id,
                Name = b.Name,
                Ordinal = b.Ordinal,
                Status = b.Status,
            }).ToList();
        }
    }
}
