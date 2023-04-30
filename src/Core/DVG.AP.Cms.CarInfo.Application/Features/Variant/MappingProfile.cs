using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetAll;
using DVG.AP.Cms.CarInfo.Application.Features.Variant.Queries.GetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VariantForCreation, Domain.Entities.Variant>();
            CreateMap<VariantForUpdate, Domain.Entities.Variant>();
            CreateMap<Domain.Entities.Variant, VariantDetailVm>();
            CreateMap<Domain.Entities.Variant, VariantVm>();
        }
    }
}
