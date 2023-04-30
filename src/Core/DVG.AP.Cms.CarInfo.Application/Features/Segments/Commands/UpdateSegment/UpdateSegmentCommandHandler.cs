using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.UpdateSegment
{
    public class UpdateSegmentCommandHandler : IRequestHandler<UpdateSegmentCommand, int>
    {
        private readonly IRepository<Domain.Entities.Segment> _segmentRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        public UpdateSegmentCommandHandler(
            IRepository<Domain.Entities.Segment> segmentRepository,
            IModelRepository modelRepository,
            IMapper mapper
        )
        {
            _segmentRepository = segmentRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateSegmentCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.Segment, new UpdateSegmentCommandValidation());

            var segment = await _segmentRepository.GetByIdAsync(request.Segment.Id, cancellationToken);
            NotFoundException.NotFound(segment, name: nameof(Domain.Entities.Segment), key: request.Segment.Id);

            _mapper.Map(request.Segment, segment, typeof(SegmentForUpdate), typeof(Domain.Entities.Segment));
            await _segmentRepository.UpdateAsync(segment, cancellationToken);
            await _segmentRepository.SaveChangesAsync(cancellationToken);


            var currentModelsInSegment = await _modelRepository.GetListInSegment(segment.Id);

            var modelIdsSetNew = request.Segment.Models.Where(p => currentModelsInSegment.All(p2 => p2.ModelId != p)).ToList();
            var modelIdsUnset = currentModelsInSegment.Where(m => !request.Segment.Models.Any(m2 => m2 == m.ModelId)).Select(m => m.ModelId).ToList();

            if (modelIdsSetNew.Any())
            {
                var models = await _modelRepository.GetListByIds(modelIdsSetNew);
                if (models != null && models.Any())
                {
                    foreach (var model in models)
                    {
                        model.SegmentId = segment.Id;
                        await _modelRepository.UpdateAsync(model, cancellationToken);
                    }
                }
            }

            if(modelIdsUnset.Any())
            {
                var models = await _modelRepository.GetListByIds(modelIdsUnset);
                if (models != null && models.Any())
                {
                    foreach (var model in models)
                    {
                        model.SegmentId = 0;
                        await _modelRepository.UpdateAsync(model, cancellationToken);
                    }
                    
                }
            }
            await _modelRepository.SaveChangesAsync(cancellationToken);

            return segment.Id;
        }
    }
}
