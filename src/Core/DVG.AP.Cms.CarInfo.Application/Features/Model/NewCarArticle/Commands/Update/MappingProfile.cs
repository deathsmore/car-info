using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContentForUpdate, ContentDetail>();
            CreateMap<ImageForUpdate, CarImage>()
                .ForMember(des => des.ObjectId, opt => opt.MapFrom(src => src.NewCarArticleId));
                //.ForMember(des => des.ImageOfObject, opt => opt.MapFrom(src => ImageOfObject.ImageOfNewCar));//T-TEMP
            CreateMap<SeoInfo, SEOInfoUpdate>().ReverseMap();

            CreateMap<NewCarArticleForUpdate, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle>()
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(des => des.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<NewCarArticleForUpdate, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBrand>();
            CreateMap<NewCarArticleForUpdate, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarModel>()
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(des => des.Images, opt => opt.MapFrom(src => src.Images));

            CreateMap<NewCarArticleForUpdate, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarVariant>()
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(des => des.Images, opt => opt.MapFrom(src => src.Images));
        }
    }
}