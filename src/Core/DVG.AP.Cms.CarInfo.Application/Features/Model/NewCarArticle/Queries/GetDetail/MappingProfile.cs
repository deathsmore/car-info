using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail
{
    public partial class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarImage, CarImageDto>()
                .ForMember(dest => dest.NewCarArticleId, opt => opt.MapFrom(src => src.ObjectId));

            CreateMap<ContentDetail, ContentDetailDto>();
            CreateMap<SeoInfo, NewCarSeoInfoDetail>();
            CreateMap<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarArticle, NewCarArticleGetDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.Images));

            CreateMap<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarBrand, NewCarArticleGetDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.Images));

            CreateMap<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarModel, NewCarArticleGetDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.Images));

            CreateMap<DVG.AP.Cms.CarInfo.Domain.Entities.NewCarVariant, NewCarArticleGetDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents))
                .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.Images));
        }
    }
}