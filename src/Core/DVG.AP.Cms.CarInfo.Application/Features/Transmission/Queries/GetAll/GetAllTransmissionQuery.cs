using DVG.AP.Cms.CarInfo.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Transmission.Queries.GetAll
{
    public class GetAllTransmissionQuery: IRequest<IReadOnlyList<TransmissionInListVm>>
    {
        public ActiveStatus Status { get; set; }
    }

    public class TransmissionInListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Ordinal { get; set; }
        public ActiveStatus Status { get; set; }
    }
}
