using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert.Models;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate.Models;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail;

namespace DVG.AP.Cms.CarInfo.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.CarInfo, CarInfoInsert>().ReverseMap();
            CreateMap<Domain.Entities.CarInfo, CarInfoUpdate>().ReverseMap();
          
     
            CreateMap<Domain.Entities.CarPrice, CarPriceForCreation>().ReverseMap();
            CreateMap<Domain.Entities.CarPrice, CarPriceForUpdate>().ReverseMap();
            CreateMap<CarInfoGetDetailVm, Domain.Entities.CarInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.Prices))
                //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ReverseMap();
        }
    }
}
