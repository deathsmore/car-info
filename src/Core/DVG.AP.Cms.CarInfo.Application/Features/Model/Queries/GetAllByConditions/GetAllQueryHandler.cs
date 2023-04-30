using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models;
using DVG.AP.Cms.CarInfo.Application.Features.Model.Specifications;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IReadOnlyList<ModelVm>>
    {
        private IRepository<Domain.Entities.Model> _modelRepository;
        public GetAllQueryHandler(
            IRepository<Domain.Entities.Model> modelRepository
        )
        {
            this._modelRepository = modelRepository;
        }
        public async Task<IReadOnlyList<ModelVm>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var modelSpec = new ModelSpec();
            modelSpec.Select();
            modelSpec.GetByStatus(request.Status);
            modelSpec.GetByBrand(request.BrandId);
            var models = await _modelRepository.ListAsync(modelSpec);
            return models.Select(m => new ModelVm()
            {
                Id = m.Id,
                BrandId = m.BrandId,
                Name = m.Name,
                Status = m.Status
            }).ToList();
        }
    }
}
