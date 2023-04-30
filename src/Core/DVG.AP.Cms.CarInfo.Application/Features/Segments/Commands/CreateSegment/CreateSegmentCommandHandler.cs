using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Commands.CreateSegment
{
    public class CreateSegmentCommandHandler : IRequestHandler<CreateSegmentCommand, int>
    {
        private readonly IRepository<Domain.Entities.Segment> _segmentRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        public CreateSegmentCommandHandler(
            IRepository<Domain.Entities.Segment> segmentRepository,
            IModelRepository modelRepository,
            IMapper mapper
        )
        {
            _segmentRepository = segmentRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateSegmentCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.Segment, new CreateSegmentCommandValidation());

            var segment = _mapper.Map<Domain.Entities.Segment>(request.Segment);
            segment = await _segmentRepository.AddAsync(segment, cancellationToken);

            var models = await _modelRepository.GetListByIds(request.Segment.Models);
            if(models != null && models.Any())
            {
                foreach (var model in models)
                {
                    model.SegmentId = segment.Id;
                    await _modelRepository.UpdateAsync(model, cancellationToken);
                }
                await _modelRepository.SaveChangesAsync(cancellationToken);
            }    
            
            return segment.Id;
        }
    }
}
