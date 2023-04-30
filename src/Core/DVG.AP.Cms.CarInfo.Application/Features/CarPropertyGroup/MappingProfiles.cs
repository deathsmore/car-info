using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfoPropertyValue.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Create;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Commands.Update;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.Filter;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup.Queries.GetList;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarPropertyGroup;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CarPropertyForCreation, CarProperty>();
        CreateMap<CarPropertyGroupForCreation, Domain.Entities.CarPropertyGroup>();

        CreateMap<CarInfoPropertyForUpdate, CarProperty>();
        CreateMap<CarPropertyGroupForUpdate, Domain.Entities.CarPropertyGroup>();

        CreateMap<CarProperty, CarPropertyDetailVm>();
        CreateMap<Domain.Entities.CarPropertyGroup, CarPropertyGroupDetailVm>()
            .ForMember(dest => dest.CarProperties, opt => opt.MapFrom(src => src.CarProperties));

        CreateMap<Domain.Entities.CarPropertyGroup, CarPropertyGroupFilterVm>();
        CreateMap<CarProperty, CarPropertyFilterVm>();

        CreateMap<CarProperty, CarPropertyGetListVm>();
        CreateMap<Domain.Entities.CarPropertyGroup, CarPropertyGroupGetListVm>()
            .ForMember(dest => dest.CarProperties, opt => opt.MapFrom(src => src.CarProperties));
    }
}