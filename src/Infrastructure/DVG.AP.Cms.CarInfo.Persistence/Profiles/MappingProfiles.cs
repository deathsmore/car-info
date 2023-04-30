using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Features.Dtos;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ResponseVms;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Persistence.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<NewCarArticleDto, NewCarArticleFilterVm>()
        //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        CreateMap<Variant, CarStructureDto>()
            .ForMember(des => des.BrandId, opt => opt.MapFrom(src => src.Model.Brand.Id))
            .ForMember(des => des.BrandName, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(des => des.ModelId, opt => opt.MapFrom(src => src.Model.Id))
            .ForMember(des => des.ModelName, opt => opt.MapFrom(src => src.Model.Name))
            .ForMember(des => des.VariantId, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.VariantName, opt => opt.MapFrom(src => src.Name));


        //CreateMap<NewCarArticle, NewCarArticleFilterDto>()
        //    .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()))
        //    .ForMember(des => des.ObjectId, opt => opt.MapFrom(src => src.ObjectId.ToString()));

        CreateMap<NewCarBrand, NewCarArticleFilterDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<NewCarModel, NewCarArticleFilterDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.BrandName, opts => opts.MapFrom(src => src.Model.Brand.Name));

        CreateMap<NewCarVariant, NewCarArticleFilterDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.ModelName, opts => opts.MapFrom(src => src.Variant.Model.Name))
            .ForMember(dest => dest.BrandName, opts => opts.MapFrom(src => src.Variant.Model.Brand.Name));

        CreateMap<CarImage, CarImageDto>();
        CreateMap<ContentDetail, ContentDetailDto>();
        CreateMap<NewCarArticle, NewCarArticleGetDetailDto>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id.ToString()));
    }
}