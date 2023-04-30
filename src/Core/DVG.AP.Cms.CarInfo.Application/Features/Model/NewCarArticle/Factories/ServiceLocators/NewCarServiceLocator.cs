using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Domain.Entities;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories.ServiceLocators;

public class NewCarServiceLocator
{
    public IMapper Mapper;
    public INewCarArticleRepository NewCarArticleRepository;
    public INewCarBrandRepository NewCarBrandRepository;
    public INewCarModelRepository NewCarModelRepository;
    public INewCarVariantRepository NewCarVariantRepository;
    public IModelRepository ModelRepository;
    public IVariantRepository VariantRepository;
    public ICarInfoRepository CarInfoRepository;
    public ICommonRepository<SeoInfo> SEOInfoRepository;
    public IUrlRepository URLRepository;
    public INewCarVariantService NewCarVariantService;
    public NewCarServiceLocator(
        IMapper mapper, 
        INewCarArticleRepository newCarArticleRepository,
        INewCarModelRepository newCarModelRepository,
        INewCarBrandRepository newCarBrandRepository,
        INewCarVariantRepository newCarVariantRepository,
        IModelRepository modelRepository,
        IVariantRepository variantRepository,
        ICarInfoRepository carInfoRepository,
        ICommonRepository<SeoInfo> seoInfoRepository,
        IUrlRepository urlRepository,
        INewCarVariantService newCarVariantService)
    {
        Mapper = mapper;
        NewCarArticleRepository = newCarArticleRepository;
        NewCarBrandRepository = newCarBrandRepository;
        NewCarModelRepository = newCarModelRepository;
        NewCarVariantRepository = newCarVariantRepository;
        ModelRepository = modelRepository;
        VariantRepository = variantRepository;
        CarInfoRepository = carInfoRepository;
        SEOInfoRepository = seoInfoRepository;
        URLRepository = urlRepository;
        NewCarVariantService = newCarVariantService;
    }
}