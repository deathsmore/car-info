using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Queries.GetBoxSettingDetail
{
    public class GetBoxSettingDetailQueryHandler : IRequestHandler<GetBoxSettingDetailQuery, NewCarBoxSettingDetailVm>
    {
        private readonly IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> _newCarBoxRepository;
        private readonly INewCarBoxDetailRepository _newCarBoxDetailRepository;
        private readonly IMapper _mapper;
        public GetBoxSettingDetailQueryHandler(
            IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> newCarBoxRepository,
            INewCarBoxDetailRepository newCarBoxDetailRepository,
            IMapper mapper
        )
        {
            this._newCarBoxRepository = newCarBoxRepository;    
            this._newCarBoxDetailRepository = newCarBoxDetailRepository;
            this._mapper = mapper;
        }

        public async Task<NewCarBoxSettingDetailVm> Handle(GetBoxSettingDetailQuery request, CancellationToken cancellationToken)
        {
            var newCarBox = await _newCarBoxRepository.GetByIdAsync(request.NewCarBoxId, cancellationToken);

            NotFoundException.NotFound(newCarBox, name: nameof(DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox), key: request.NewCarBoxId);

            var newCarBoxDetails = await _newCarBoxDetailRepository.GetByNewCarBox(request.NewCarBoxId);


            return new NewCarBoxSettingDetailVm()
            {
                NewCarBoxId = request.NewCarBoxId,
                Items = newCarBoxDetails.Select(x => new NewCarBoxDetailItemVm()
                {
                    Id = x.Id,
                    NewCarBoxId = x.NewCarBoxId,
                    ObjectId = x.ObjectId.ToString(),
                    ObjectType = x.ObjectType,

                    Ordinal = x.Ordinal,
                    NewCarArticleTitle = x.NewCarArticleTitle,
                    NewCarArticleId = x.NewCarArticleId.ToString(),
                    NewCarStatus = x.NewCarStatus,
                    PublishedDate = x.PublishedDate
                }).ToList()
            };
        }
    }
}