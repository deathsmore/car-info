using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorUpdate
{
    public class CarColorUpdateCommandHandler : IRequestHandler<CarColorUpdateCommand>
    {
        private readonly IRepository<Domain.Entities.CarColor> _carColorRepository;
        private readonly IMapper _mapper;

        public CarColorUpdateCommandHandler(IRepository<Domain.Entities.CarColor> carColorRepository, IMapper mapper)
        {
            _carColorRepository = carColorRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CarColorUpdateCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.CarColorUpdate, new CarColorUpdateCommandValidation());

            var carColor = await _carColorRepository.GetByIdAsync(request.CarColorUpdate.Id, cancellationToken);
            NotFoundException.NotFound(carColor, name: nameof(Domain.Entities.CarColor),
                key: request.CarColorUpdate.Id);

            // check code is exist on db
            var carColorCheck = await _carColorRepository.GetBySpecAsync(
                new CarColorCheckCodeExistSpecForUpdate(request.CarColorUpdate.Id, request.CarColorUpdate.Code),
                cancellationToken);
            if (carColorCheck != null && carColorCheck.Id > 0)
            {
                throw new ConflictException(nameof(Domain.Entities.CarColor),
                    request.CarColorUpdate.Code);
                
            }


            _mapper.Map(request.CarColorUpdate, carColor, typeof(CarColorUpdate),
                typeof(Domain.Entities.CarColor));

            await _carColorRepository.UpdateAsync(carColor, cancellationToken);
            await _carColorRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}