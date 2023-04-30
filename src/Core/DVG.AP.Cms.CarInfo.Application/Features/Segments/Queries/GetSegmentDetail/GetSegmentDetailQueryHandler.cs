using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Segments.Queries.GetSegmentDetail
{
    public class GetSegmentDetailQueryHandler : IRequestHandler<GetSegmentDetailQuery, SegmentDetailVm>
    {
        private readonly IRepository<Domain.Entities.Segment> _segmentRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        public GetSegmentDetailQueryHandler(
            IRepository<Domain.Entities.Segment> segmentRepository,
            IModelRepository modelRepository, 
            IMapper mapper)
        {
            _segmentRepository = segmentRepository;
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<SegmentDetailVm> Handle(GetSegmentDetailQuery request, CancellationToken cancellationToken)
        {
            var segment = await _segmentRepository.GetByIdAsync(request.Id);
            NotFoundException.NotFound(segment, name: nameof(Segments), key: request.Id);
            var segmentVm = _mapper.Map<SegmentDetailVm>(segment);
            segmentVm.Models = await _modelRepository.GetListInSegment(segment.Id);
            return segmentVm;
        }
    }
}
