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

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarPage.Commands.UpdateNewCarPage
{
    public class UpdateNewCarPageCommandHandler : IRequestHandler<UpdateNewCarPageCommand>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Domain.Entities.NewCarPage> _newCarPageRepository;


        public UpdateNewCarPageCommandHandler(IMapper mapper,
            IRepository<Domain.Entities.NewCarPage> newCarPageRepository)
        {
            _mapper = mapper;
            _newCarPageRepository = newCarPageRepository;
        }

        public async Task<Unit> Handle(UpdateNewCarPageCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.NewCarPageForUpdate, new UpdateNewCarPageCommandValidator());


            var newCarPage = await _newCarPageRepository.GetByIdAsync(request.NewCarPageForUpdate.Id, cancellationToken);
            NotFoundException.NotFound(newCarPage, name: nameof(Domain.Entities.NewCarPage),
                key: request.NewCarPageForUpdate.Id);
            _mapper.Map(request.NewCarPageForUpdate, newCarPage, typeof(NewCarPageForUpdate),
                typeof(Domain.Entities.NewCarPage));
            await _newCarPageRepository.UpdateAsync(newCarPage, cancellationToken);
            return Unit.Value;
        }
    }
}
