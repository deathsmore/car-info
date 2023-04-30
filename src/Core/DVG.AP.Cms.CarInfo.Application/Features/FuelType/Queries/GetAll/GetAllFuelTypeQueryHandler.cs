using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.FuelType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.FuelType.Queries.GetAll
{
    public class GetAllFuelTypeQueryHandler : IRequestHandler<GetAllFuelTypeQuery, IReadOnlyList<FuelTypeInListVm>>
    {
        private readonly IRepository<Domain.Entities.FuelType> _fuelTypeRepository;
        private IMapper _mapper;
        public GetAllFuelTypeQueryHandler(IRepository<Domain.Entities.FuelType> fuelTypeRepository, IMapper mapper)
        {
            _fuelTypeRepository = fuelTypeRepository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<FuelTypeInListVm>> Handle(GetAllFuelTypeQuery request, CancellationToken cancellationToken)
        {
            var fuelTypeSpec = new FuelTypeSpec();
            fuelTypeSpec.Select();

            var fuelTypes = await _fuelTypeRepository.ListAsync(fuelTypeSpec);
            return _mapper.Map<List<FuelTypeInListVm>>(fuelTypes);
        }
    }
}
