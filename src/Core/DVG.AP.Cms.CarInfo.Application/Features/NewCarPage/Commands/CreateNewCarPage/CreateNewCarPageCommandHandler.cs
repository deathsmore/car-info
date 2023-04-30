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

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.CreateNewCarPage
{
    public class CreateNewCarPageCommandHandler : IRequestHandler<CreateNewCarPageCommand, int>
    {
        private readonly IRepository<Domain.Entities.NewCarPage> _newCarPageRepository;
        private readonly IMapper _mapper;

        public CreateNewCarPageCommandHandler(IRepository<Domain.Entities.NewCarPage> newCarPageRepository, IMapper mapper)
        {
            _newCarPageRepository = newCarPageRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateNewCarPageCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.NewCarPageForCreation, new CreateNewCarPageValidator());
            var newCarPageCreate = _mapper.Map<Domain.Entities.NewCarPage>(request.NewCarPageForCreation);
            await _newCarPageRepository.AddAsync(newCarPageCreate, cancellationToken);

            return newCarPageCreate.Id;
        }
    }
}
