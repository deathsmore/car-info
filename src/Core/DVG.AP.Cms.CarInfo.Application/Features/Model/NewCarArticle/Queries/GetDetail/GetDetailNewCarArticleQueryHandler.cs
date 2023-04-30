using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail.Models;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarImage;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.SEOInfo;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.Variant;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Queries.GetDetail;

public class
    GetDetailNewCarArticleQueryHandler : IRequestHandler<GetDetailNewCarArticleQuery, NewCarArticleGetDetailDto>
{
    private readonly INewCarArticleRepository _newCarArticleRepository;
    private readonly IRepository<CarImage> _carImageRepository;
    private readonly IMapper _mapper;
    private readonly ICommonRepository<SeoInfo> _seoInfoRepository;
    protected readonly IRepository<Domain.Entities.Model> _modelRepository;
    protected readonly IRepository<Domain.Entities.Variant> _variantRepository;
    private readonly IUrlRepository _urlRepository;
    private readonly NewCarArticleFactory _newCarArticleFactory;
    public GetDetailNewCarArticleQueryHandler(
        INewCarArticleRepository newCarRepository,
        IRepository<CarImage> carImageRepository,
        IMapper mapper,
        ICommonRepository<SeoInfo> seoInfoRepository,
        IRepository<Domain.Entities.Model> modelRepository,
        IRepository<Domain.Entities.Variant> variantRepository,
        IUrlRepository urlRepository,
        NewCarArticleFactory newCarArticleFactory
        )
    {
        _newCarArticleRepository = newCarRepository;
        _carImageRepository = carImageRepository;
        _mapper = mapper;
        _seoInfoRepository = seoInfoRepository;
        _modelRepository = modelRepository;
        _variantRepository = variantRepository;
        _urlRepository = urlRepository;
        _newCarArticleFactory = newCarArticleFactory;
    }
    public GetDetailNewCarArticleQueryHandler(
        IMapper mapper,
        IUrlRepository urlRepository,
        NewCarArticleFactory? newCarArticleFactory
    )
    {
        _mapper = mapper;
        _urlRepository = urlRepository;
        _newCarArticleFactory = newCarArticleFactory;
    }
    public async Task<NewCarArticleGetDetailDto> Handle(GetDetailNewCarArticleQuery request,
        CancellationToken cancellationToken)
    {
        NewCarArticleAbstract newCarArticle = _newCarArticleFactory.CreateNewCarArticle(request.Type);
        var newCarEntity = await newCarArticle.GetDetailAsync(request.Id);
        NotFoundException.NotFound(newCarEntity, name: nameof(NewCarArticle), key: request.Id);
        
        //newCarArticle.SetNewCarEntity(newCarEntity);
        //var newCarArticleDetail = _mapper.Map<NewCarArticleGetDetailDto>(newCarArticle.NewCarEntity);
        newCarArticle.SetNewCarEntity(newCarEntity);
        var x = newCarArticle.NewCarEntity;
        
        var newCarArticleDetail = _mapper.Map<NewCarArticleGetDetailDto>(newCarEntity);
        
        newCarArticleDetail.Type = request.Type;
        var urlNewCarSpec = new UrlNewCarSpec();
        urlNewCarSpec.GetByNewCar(newCarEntity.Id, newCarArticle.ObjectType);
        var url = await _urlRepository.GetBySpecAsync(urlNewCarSpec, cancellationToken);
        newCarArticleDetail.Url = url?.Slug;
     
        return newCarArticleDetail;
    }

  
}