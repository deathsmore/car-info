using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update.Models;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarImage;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.NewCarArticle;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.SEOInfo;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.UrlSpec;
using DVG.AP.Cms.CarInfo.Domain.Entities;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Commands.Update
{
    public class UpdateNewCarArticleCommandHandler : IRequestHandler<UpdateNewCarArticleCommand, int>
    {
        private readonly INewCarArticleRepository _newCarArticleRepository;
        private readonly IRepository<CarImage> _carImageRepository;
        private readonly IMapper _mapper;
        private readonly ICommonRepository<SeoInfo> _seoInfoRepository;
        private readonly IUrlRepository _urlRepository;
        private readonly NewCarArticleFactory _newCarArticleFactory;
        public UpdateNewCarArticleCommandHandler(
            INewCarArticleRepository newCarArticleRepository,
            IRepository<CarImage> carImageRepository,
            IMapper mapper, ICommonRepository<SeoInfo> seoInfoRepository,
            IUrlRepository urlRepository,
            NewCarArticleFactory newCarArticleFactory
        )
        {
            _newCarArticleRepository = newCarArticleRepository;
            _carImageRepository = carImageRepository;
            _mapper = mapper;
            _seoInfoRepository = seoInfoRepository;
            _urlRepository = urlRepository;
            _newCarArticleFactory = newCarArticleFactory;
        }

        public UpdateNewCarArticleCommandHandler(
            IUrlRepository urlRepository,
            NewCarArticleFactory newCarArticleFactory
        )
        {
            _urlRepository = urlRepository;
            _newCarArticleFactory = newCarArticleFactory;
        }

        public async Task<int> Handle(UpdateNewCarArticleCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.NewCarArticle, new UpdateNewCarArticleCommandValidation());

            NewCarArticleAbstract newCarArticle = _newCarArticleFactory.CreateNewCarArticle(request.NewCarArticle.Type);
            await newCarArticle.Update(request.NewCarArticle);

           

            #region Lưu thông tin Url
            var urlNewCarSpec = new UrlNewCarSpec();
            urlNewCarSpec.GetByNewCar(newCarArticle.NewCarEntity.Id, newCarArticle.ObjectType);
            var url = await _urlRepository.GetBySpecAsync(urlNewCarSpec, cancellationToken);

            if (url == null)
            {
                var slugExisted = await _urlRepository.GetBySlug(request.NewCarArticle.Url);
                await _urlRepository.AddAsync(new Url
                {
                    ObjectId = newCarArticle.NewCarEntity.Id,
                    ObjectType = newCarArticle.ObjectType,
                    Slug = slugExisted == null ? request.NewCarArticle.Url : $"{request.NewCarArticle.Url}-{request.NewCarArticle.Id}-{newCarArticle.ObjectType.GetHashCode()}",
                    CreatedDate = DateTime.Now
                }, cancellationToken);
            }
            #endregion
            return 1;
        }
    }
}