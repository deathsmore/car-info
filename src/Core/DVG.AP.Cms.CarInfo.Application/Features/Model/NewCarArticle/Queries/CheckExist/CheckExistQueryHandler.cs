using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.CheckExist
{
    public class CheckExistQueryHandler : IRequestHandler<CheckExistQuery, bool>
    {
        private readonly IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle> _newCarRepository;
        private readonly IMapper _mapper;
        public CheckExistQueryHandler(
            IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle> newCarRepository,
            IMapper mapper
        )
        {
            _newCarRepository = newCarRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CheckExistQuery request, CancellationToken cancellationToken)
        {
            var newCarDetailSpec = new NewCarArticleSpec();
            newCarDetailSpec.Select();
            newCarDetailSpec.GetByObjectIdAndType(Convert.ToInt64(request.ObjectId), request.Type);

            var newCarArticle = await _newCarRepository.GetBySpecAsync(newCarDetailSpec);

            return newCarArticle != null;
        }
    }
}
