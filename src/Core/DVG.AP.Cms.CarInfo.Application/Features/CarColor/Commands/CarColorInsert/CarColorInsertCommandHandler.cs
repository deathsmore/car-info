using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarColor.Specifications;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarColor.Commands.CarColorInsert
{
    internal class CarColorInsertCommandHandler : IRequestHandler<CarColorInsertCommand, int>
    {
        private readonly IRepository<Domain.Entities.CarColor> _carColorRepository;
        private readonly IMapper _mapper;

        public CarColorInsertCommandHandler(IRepository<Domain.Entities.CarColor> carColorRepository, IMapper mapper)
        {
            _carColorRepository = carColorRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CarColorInsertCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.CarColorInsert, new CarColorInsertCommandValidation());

            // check code is exist on db
            var carColor =
                await _carColorRepository.GetBySpecAsync(new CarColorCheckCodeExistSpec(request.CarColorInsert.Code),
                    cancellationToken);
            if (carColor != null && carColor.Id > 0)
            {
                throw new ConflictException(nameof(Domain.Entities.CarColor),
                    request.CarColorInsert.Code);
            }


            carColor = _mapper.Map<Domain.Entities.CarColor>(request.CarColorInsert);
            carColor = await _carColorRepository.AddAsync(carColor, cancellationToken);
            await _carColorRepository.SaveChangesAsync();
            return carColor.Id;
        }
    }
}