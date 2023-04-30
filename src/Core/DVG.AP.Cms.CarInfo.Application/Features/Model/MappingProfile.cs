using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.Model.Queries.GetAllByConditions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Model
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Model, ModelVm>();
            
        }
    }
}
