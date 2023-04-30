using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Specifications;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using DVG.AutoPortal.Specification;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarBox.Commands.SetDetails
{
    public class SetDetailsCommandHandler : IRequestHandler<SetDetailsCommand, int>
    {
        private readonly IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> _newCarBoxRepository;
        private readonly IRepository<NewCarBoxDetail> _newCarBoxDetailRepository;
        private readonly IMapper _mapper;
        private readonly  NewCarBoxDetailSpec _newCarBoxDetailSpec;

        public SetDetailsCommandHandler(
            IRepository<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox> newCarBoxRepository,
            IRepository<NewCarBoxDetail> newCarBoxDetailRepository,
            IMapper mapper
        )
        {
            this._newCarBoxRepository = newCarBoxRepository;
            this._newCarBoxDetailRepository = newCarBoxDetailRepository;
            this._mapper = mapper;
            _newCarBoxDetailSpec = new NewCarBoxDetailSpec();
        }

        public async Task<int> Handle(SetDetailsCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.NewCarBoxSetDetail, new SetDetailsCommandValidation());

            var newCarBox =
                await _newCarBoxRepository.GetByIdAsync(request.NewCarBoxSetDetail.NewCarBoxId, cancellationToken);

            NotFoundException.NotFound(newCarBox, name: nameof(DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBox),
                key: request.NewCarBoxSetDetail.NewCarBoxId);

            await Guard.Against.Validate(request.NewCarBoxSetDetail,
                new SetDetailsCommandValidation(newCarBox.NumberDisplay));

            var newCarBoxDetails = _mapper.Map<List<NewCarBoxDetail>>(request.NewCarBoxSetDetail.Items);

            newCarBoxDetails!.ForEach(p => { p.NewCarBoxId = request.NewCarBoxSetDetail.NewCarBoxId; });

            _newCarBoxDetailSpec.GetByNewCarBoxId(request.NewCarBoxSetDetail.NewCarBoxId);
            _newCarBoxDetailSpec.SelectAllField();

            var newCarBoxDetailsExists =
                await _newCarBoxDetailRepository.ListAsync(_newCarBoxDetailSpec, cancellationToken);


            await _newCarBoxDetailRepository.DeleteRangeAsync(newCarBoxDetailsExists, cancellationToken);
            await _newCarBoxDetailRepository.AddRangeAsync(newCarBoxDetails);
            await _newCarBoxDetailRepository.SaveChangesAsync(cancellationToken);
            return 1;
        }
    }
}