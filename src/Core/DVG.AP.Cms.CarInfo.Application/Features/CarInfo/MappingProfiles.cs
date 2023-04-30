using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert.Models;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate.Models;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetCarInfos;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.CarPrice, CarPriceForGet>();
        CreateMap<Domain.Entities.CarImage, CarImageForGet>()
            .ForMember(des => des.CarInfoId, opt => opt.MapFrom(src => src.ObjectId));
        CreateMap<CarImageForCreation, Domain.Entities.CarImage>()
            .ForMember(des => des.ImageOfObject, opt => opt.MapFrom(src => ImageOfObject.ImageOfCarInfo));
        CreateMap<CarImageForUpdate, Domain.Entities.CarImage>().ForMember(des => des.ImageOfObject,
                opt => opt.MapFrom(src => ImageOfObject.ImageOfCarInfo))
            .ForMember(des => des.ObjectId, opt => opt.MapFrom(src => src.CarInfoId));
        //CarInfoInsert
        //CreateMap<CarInfoInsert,Domain.Entities.CarInfo>()
        //    .ForMember(c => c.Variant, b => b.DoNotValidate());
        CreateMap<Domain.Entities.CarInfo, CarInfoGetDetailVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            //.ForMember(dest => dest.Prices, opt => opt.MapFrom(src => src.Prices))
            .ForMember(des => des.ModelId, opt => opt.MapFrom(src => src.Variant.ModelId))
            .ForMember(des => des.BrandId, opt => opt.MapFrom(src => src.Variant.Model.BrandId));
        //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        CreateMap<Domain.Entities.CarInfo, CarInfoVm>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ReverseMap();
    }
}