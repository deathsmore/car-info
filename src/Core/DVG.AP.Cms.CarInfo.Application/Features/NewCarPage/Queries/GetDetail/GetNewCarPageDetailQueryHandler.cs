using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Queries.GetDetail
{
    public class GetNewCarPageDetailQueryHandler : IRequestHandler<GetNewCarPageDetailQuery, NewCarPageDetailVm>
    {
        private readonly INewCarPageRepository _newCarPageRepository;
        private readonly IMapper _mapper;

        public GetNewCarPageDetailQueryHandler(
            INewCarPageRepository newCarPageRepository, IMapper mapper)
        {
            _newCarPageRepository = newCarPageRepository;
            _mapper = mapper;
        }

        public async Task<NewCarPageDetailVm> Handle(GetNewCarPageDetailQuery request,
            CancellationToken cancellationToken)
        {
            var newCarPageDetail = await _newCarPageRepository.GetByIdAsync(request.Id, cancellationToken);
            NotFoundException.NotFound(newCarPageDetail, name: nameof(NewCarPage), key: request.Id);
            return _mapper.Map<NewCarPageDetailVm>(newCarPageDetail);
        }
    }
}
