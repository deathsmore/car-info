using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Create
{
    public class CreateNewCarArticleCommandHandler : IRequestHandler<CreateNewCarArticleCommand, long>
    {
        private readonly INewCarArticleRepository _newCarArticleRepository;
        private readonly IRepository<CarImage> _carImageRepository;
        private readonly IMapper _mapper;
        private readonly ICommonRepository<SeoInfo> _seoInfoRepository;
        private readonly IUrlRepository _urlRepository;
        private readonly NewCarArticleFactory _newCarArticleFactory;

        public CreateNewCarArticleCommandHandler(
            INewCarArticleRepository newCarArticleRepository,
            IRepository<CarImage> carImageRepository,
            IMapper mapper,
            ICommonRepository<SeoInfo> seoInfoRepository,
            IUrlRepository urlRepository,
            NewCarArticleFactory newCarArticleFactory)
        {
            _newCarArticleRepository = newCarArticleRepository;
            _carImageRepository = carImageRepository;
            _mapper = mapper;
            _seoInfoRepository = seoInfoRepository;
            _urlRepository = urlRepository;
            _newCarArticleFactory = newCarArticleFactory;
        }

        public CreateNewCarArticleCommandHandler(NewCarArticleFactory newCarArticleFactory)
        {
            _newCarArticleFactory = newCarArticleFactory;
        }

        public async Task<long> Handle(CreateNewCarArticleCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.NewCarArticle, new CreateNewCarArticleCommandValidation());

            NewCarArticleAbstract newCarArticle = _newCarArticleFactory.CreateNewCarArticle(request.NewCarArticle.Type);
            
            #region Check exist article
            var  checkExisted = await newCarArticle.Get(request.NewCarArticle.BrandId);
            if (checkExisted != null)
                throw new ConflictException(nameof(NewCarBrand), null);
            #endregion

            var id = await newCarArticle.Insert(request.NewCarArticle);
            return id;
        }
    }
}