using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.Transmission.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Transmission
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Transmission, TransmissionInListVm>();

        }
    }
}
