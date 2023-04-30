using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContentForCreation, ContentDetail>();
            CreateMap<ImageForCreation, CarImage>();
                //.ForMember(des => des.ImageOfObject, opt => opt.MapFrom(src => ImageOfObject.ImageOfNewCar));//T-TEMP
            //.ForMember(dest => dest.ObjectId, opt => opt.MapFrom(src => Convert.ToInt64(src.)));

            CreateMap<NewCarArticleForCreation, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                ;
            CreateMap<NewCarArticleForCreation, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBrand>();
            CreateMap<NewCarArticleForCreation, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarModel>();
            //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => (NewCarModelImage)i)));
            //.ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
            //;//T-TEMP
            CreateMap<NewCarArticleForCreation, DVG.AP.Cms.CarInfo.Domain.Entities.NewCarVariant>();
            CreateMap<NewCarArticleSEOInfoForCreation, SeoInfo>();
        }
    }
}