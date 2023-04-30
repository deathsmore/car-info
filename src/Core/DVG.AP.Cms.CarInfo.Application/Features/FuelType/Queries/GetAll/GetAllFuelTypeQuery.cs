using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.FuelType.Queries.GetAll
{
    public class GetAllFuelTypeQuery : IRequest<IReadOnlyList<FuelTypeInListVm>>
    {
        public ActiveStatus Status { get; set; }
    }

    public class FuelTypeInListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
