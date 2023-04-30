using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Dtos;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Profiles
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<NewCarArticleDto, NewCarArticleFilterDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            // .ForMember(dest => dest.ModelId, opt => opt.MapFrom(src => Convert.ToInt32(src.ModelId)))
            // .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => Convert.ToInt32(src.BrandId)));
        }
    }
}