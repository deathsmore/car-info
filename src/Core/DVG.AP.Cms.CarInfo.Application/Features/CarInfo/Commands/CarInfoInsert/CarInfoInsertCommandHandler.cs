using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence.Base;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;
using DVG.AP.Cms.CarInfo.Application.Features.Dtos;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;
using System.Linq.Expressions;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoInsert
{
    public class CarInfoInsertCommandHandler : IRequestHandler<CarInfoInsertCommand, long>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IRepository<Domain.Entities.CarImage> _carImageRepository;
        private readonly IMapper _mapper;
        private readonly IUrlRepository _urlRepository;
        private readonly NewCarArticleFactory _newCarArticleFactory;
        private readonly INewCarArticleRepository _newCarArticleRepository;
        private readonly INewCarModelRepository _newCarModelRepository;
        private readonly INewCarVariantRepository _newCarVariantRepository;
        private readonly IModelPropertySummariesService _modelPropertySummariesService;
        public CarInfoInsertCommandHandler(
            ICarInfoRepository carInfoRepository,
            IRepository<Domain.Entities.CarImage> carImageRepository,
            IMapper mapper,
            IUrlRepository urlRepository,
            NewCarArticleFactory newCarArticleFactory,
            INewCarArticleRepository newCarArticleRepository,
            INewCarModelRepository newCarModelRepository,
            INewCarVariantRepository newCarVariantRepository,
            IModelPropertySummariesService modelPropertySummariesService
        )
        {
            _carInfoRepository = carInfoRepository;
            _carImageRepository = carImageRepository;
            _urlRepository = urlRepository;
            _mapper = mapper;
            _newCarArticleFactory = newCarArticleFactory;
            _newCarArticleRepository = newCarArticleRepository;
            _newCarModelRepository = newCarModelRepository;
            _newCarVariantRepository = newCarVariantRepository;
            _modelPropertySummariesService = modelPropertySummariesService;
        }

        public async Task<long> Handle(CarInfoInsertCommand request, CancellationToken cancellationToken)
        {
            var checkExistedSpec = new GetCarInfosSpec();
            checkExistedSpec.GetByVariant(request.CarInfoInsert.VariantId);
            checkExistedSpec.GetByYear(request.CarInfoInsert.Year);
            var carInfoCheckExisted = await _carInfoRepository.ListAsync(checkExistedSpec, cancellationToken);

            if (carInfoCheckExisted.Any())
            {
                throw new ConflictException(nameof(Domain.Entities.CarInfo),
                    new { request.CarInfoInsert.VariantId, request.CarInfoInsert.Year });
            }

            await Guard.Against.Validate(request.CarInfoInsert, new CarInfoInsertCommandValidation());
            var carInfo = _mapper.Map<Domain.Entities.CarInfo>(request.CarInfoInsert);

            var latestCarInfo = await _carInfoRepository.GetLatest(request.CarInfoInsert.VariantId);
            if (latestCarInfo == null)
            {
                carInfo.IsLatest = true;
            }
            else if (latestCarInfo.Year < carInfo.Year)
            {
                carInfo.IsLatest = true;
                latestCarInfo.IsLatest = false;
                await _carInfoRepository.UpdateAsync(latestCarInfo);
            }
            carInfo = await _carInfoRepository.AddAsync(carInfo, cancellationToken);

            #region Xử lý url db common
            var slugExisted = await _urlRepository.GetBySlug(request.CarInfoInsert.Url);
            await _urlRepository.AddAsync(new Url
            {
                ObjectId = request.CarInfoInsert.Id,
                ObjectType = ObjectType.CarInfo,
                Slug = slugExisted == null ? request.CarInfoInsert.Url : $"{request.CarInfoInsert.Url}-{request.CarInfoInsert.Id}-{ObjectType.CarInfo.GetHashCode()}",
                CreatedDate = DateTime.Now
            }, cancellationToken);

            await _urlRepository.SaveChangesAsync(cancellationToken);
            #endregion



            #region Update lại price range cho các bài newCar liên quan
            var newCarModelEntity = await _newCarModelRepository.GetByModelId(request.CarInfoInsert.ModelId);
            if (newCarModelEntity != null)
            {
                NewCarArticleAbstract newCarModel = _newCarArticleFactory.CreateNewCarArticle(NewCarArticleType.Model);
                newCarModel.SetNewCarEntity(newCarModelEntity);
                await newCarModel.CalcPriceRange();
                await _newCarModelRepository.UpdateAsync(newCarModelEntity);
            }

            var newCarVariantEntity = await _newCarVariantRepository.GetByVariantId(request.CarInfoInsert.VariantId);
            if (newCarVariantEntity != null)
            {
                NewCarArticleAbstract newCarVariant = _newCarArticleFactory.CreateNewCarArticle(NewCarArticleType.Variant);
                newCarVariant.SetNewCarEntity(newCarVariantEntity);
                await newCarVariant.CalcPriceRange();
                await _newCarVariantRepository.UpdateAsync(newCarVariantEntity);
            }
            #endregion

            #region Sync du lieu bang ModelPropertySummaries
            if(carInfo.Status == ActiveStatus.Active)
            {
                await _modelPropertySummariesService.SyncSummarySpec(request.CarInfoInsert.ModelId);
            }
            #endregion

            return carInfo.Id;
        }
    }
}