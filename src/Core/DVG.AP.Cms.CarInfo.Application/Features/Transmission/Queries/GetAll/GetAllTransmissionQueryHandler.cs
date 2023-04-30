using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.Transmission;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Transmission.Queries.GetAll
{
    public class GetAllTransmissionQueryHandler : IRequestHandler<GetAllTransmissionQuery, IReadOnlyList<TransmissionInListVm>>
    {
        private readonly IRepository<Domain.Entities.Transmission> _transmissionRepository;
        private IMapper _mapper;
        public GetAllTransmissionQueryHandler(IRepository<Domain.Entities.Transmission> transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<TransmissionInListVm>> Handle(GetAllTransmissionQuery request, CancellationToken cancellationToken)
        {
            var transmissionSpec = new TransmissionSpec();
            transmissionSpec.Select();
            transmissionSpec.GetByStatus(request.Status);

            var transmissions = await _transmissionRepository.ListAsync(transmissionSpec);
            return _mapper.Map<List<TransmissionInListVm>>(transmissions);
        }
    }
}
