using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.FuelType.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.FuelType
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.FuelType, FuelTypeInListVm>();

        }
    }
}
